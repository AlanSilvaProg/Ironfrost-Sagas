# ☁️ Infraestrutura AWS para WebSocket — RTS MMO

## O Problema: WebSocket é Stateful

A grande diferença entre REST e WebSocket na infra é:

```
REST (stateless):
  Request → qualquer servidor → Response → conexão morre
  Load balancer distribui livremente entre N instâncias

WebSocket (stateful):
  Handshake → conexão PERSISTENTE com servidor específico
  O jogador FICA conectado a uma instância
  Se a instância cair → jogador desconecta
```

Isso muda completamente como você pensa a infraestrutura.

---

## 🏆 Recomendação: ECS Fargate (não EKS)

| Serviço | Veredicto | Por quê |
|---------|-----------|---------|
| **ECS Fargate** | ✅ **Recomendado** | Simples, serverless containers, você já tem experiência |
| **EKS** | ⚠️ Overkill (por agora) | Kubernetes é poderoso mas complexo demais para começar |
| **EC2 puro** | ⚠️ Muito manual | Você gerencia tudo: deploy, scaling, health checks |
| **Lambda** | ❌ Impossível | Não suporta conexões persistentes (timeout 15min) |
| **App Runner** | ❌ Limitado | Não suporta WebSocket adequadamente |
| **GameLift** | ⚠️ Nicho | Otimizado para jogos de sessão (FPS), não para MMO persistente |

> [!IMPORTANT]
> **EKS não é "melhor" que ECS — é mais complexo.** EKS faz sentido quando você precisa de recursos avançados do Kubernetes (service mesh, custom operators, multi-cloud). Para um jogo MMO, ECS Fargate faz tudo que você precisa com 1/3 da complexidade operacional.

---

## 🏗️ Arquitetura Completa

```
                         ┌──────────────┐
                         │   Route 53   │
                         │  (DNS)       │
                         └──────┬───────┘
                                │
                         ┌──────▼───────┐
                         │     ALB      │
                         │ (Application │
                         │  Load        │
                         │  Balancer)   │
                         │              │
                         │ • Sticky     │
                         │   Sessions   │
                         │ • SSL/TLS    │
                         │ • Health     │
                         │   Checks     │
                         └──┬───┬───┬───┘
                            │   │   │
               ┌────────────┘   │   └────────────┐
               │                │                │
        ┌──────▼──────┐ ┌──────▼──────┐ ┌───────▼─────┐
        │  ECS Task   │ │  ECS Task   │ │  ECS Task   │
        │  (Game      │ │  (Game      │ │  (Game      │
        │   Server)   │ │   Server)   │ │   Server)   │
        │             │ │             │ │             │
        │ ~2000-5000  │ │ ~2000-5000  │ │ ~2000-5000  │
        │ connections │ │ connections │ │ connections  │
        └──────┬──────┘ └──────┬──────┘ └───────┬─────┘
               │               │                │
               └───────────┐   │   ┌────────────┘
                           │   │   │
                    ┌──────▼───▼───▼──────┐
                    │   ElastiCache       │
                    │   (Redis)           │
                    │                     │
                    │ • Pub/Sub entre     │
                    │   instâncias        │
                    │ • Session state     │
                    │ • Missed events     │
                    │   buffer            │
                    └──────────┬──────────┘
                               │
                    ┌──────────▼──────────┐
                    │      RDS            │
                    │   (PostgreSQL)      │
                    │                     │
                    │ • Dados persistentes│
                    │ • Villages, Users   │
                    │ • Battle reports    │
                    └─────────────────────┘
```

---

## 🔑 Componentes e Papéis

### 1. ALB (Application Load Balancer)

O ALB da AWS suporta WebSocket **nativamente**. Configurações críticas:

```
Idle Timeout:         3600s (1 hora — padrão é 60s!)
Sticky Sessions:      HABILITADO (cookie-based)
Health Check Path:    /health (HTTP, não WS)
Target Group:         IP mode (obrigatório para Fargate)
Listener:             443 (wss://) → Target Group
```

> [!WARNING]
> **Idle Timeout padrão = 60 segundos!** Se o jogador ficar 60s sem enviar nada, o ALB fecha a conexão. Aumente para 3600s e implemente **heartbeat** no WebSocket (ping/pong a cada 30s).

### 2. ECS Fargate (Game Server)

```
Task Definition:
  CPU:    1 vCPU  (ou 2 para produção)
  Memory: 2 GB    (ou 4 GB para produção)
  Port:   8080

Service:
  Desired Count:  2   (mínimo para alta disponibilidade)
  Min:            2
  Max:            20  (auto-scaling)
  
Auto Scaling:
  Trigger:  Número de conexões ativas    ← métrica custom
  Scale Up: quando > 3000 conexões/task
  Scale Down: quando < 1000 conexões/task
```

> [!TIP]
> **Scaling de WebSocket é baseado em CONEXÕES, não em CPU/memória.** Um servidor WebSocket tipicamente gasta pouca CPU mas muita memória (cada conexão = ~50KB de state). Publique uma métrica custom no CloudWatch com o número de conexões ativas.

### 3. ElastiCache Redis — O Componente Mais Importante

**Por que precisa de Redis?** Porque com 2+ instâncias, o Jogador A pode estar no Server 1 e o Jogador B no Server 2. Quando A ataca B, o Server 1 precisa avisar o Server 2.

```
┌──────────────┐         ┌──────────────┐
│  Server 1    │         │  Server 2    │
│              │         │              │
│  Jogador A ──┼── envia ─► Redis ──────┼──► Jogador B
│  (atacante)  │  ataque │  Pub/Sub     │  (defensor)
│              │         │              │
└──────────────┘         └──────────────┘

Fluxo:
1. Jogador A (Server 1) envia comando de ataque
2. Server 1 processa → publica evento no Redis channel
3. Server 2 recebe o evento via Redis Pub/Sub
4. Server 2 notifica Jogador B: "ataque incoming!"
```

**Configuração Redis:**
```
Tipo:               ElastiCache Serverless (Redis)
                    ou r7g.large (se quiser instância fixa)
Multi-AZ:           Sim
Pub/Sub Channels:   
  - world:{world_id}:events     (eventos globais do mundo)
  - village:{village_id}:events (eventos específicos)
  - aett:{aett_id}:chat         (chat da tribo)
```

### 4. RDS (PostgreSQL)

```
Tipo:       Aurora Serverless v2  (escala automática)
            ou db.r6g.large       (instância fixa, mais previsível)
Multi-AZ:   Sim
Storage:    gp3, auto-scaling
```

> [!NOTE]
> O game server **NÃO deve fazer queries SQL a cada ação do jogador.** Use Redis como cache de estado ativo e sincronize com o banco periodicamente (write-behind) ou em momentos chave (login, logout, conquista).

---

## 📊 Comparativo Detalhado: ECS vs EKS vs EC2

### Complexidade Operacional

```
                    Simples ◄──────────────────────► Complexo
                    
EC2 + Docker manual │████████████████████████████████████│
                    │                                    │
ECS Fargate         │████████████░░░░░░░░░░░░░░░░░░░░░░░│  ← sweet spot
                    │                                    │
EKS                 │█████████████████████████████░░░░░░░│
                    │                                    │
EKS + Istio + Helm  │████████████████████████████████████│
```

### Tabela Comparativa

| Critério | ECS Fargate | EKS | EC2 Direto |
|----------|------------|-----|------------|
| **Setup inicial** | ~1 dia | ~3-5 dias | ~2 dias |
| **WebSocket support** | ✅ Nativo via ALB | ✅ Nativo via ALB | ✅ Direto |
| **Auto-scaling** | ✅ Simples (Service Auto Scaling) | ✅ Poderoso (HPA, KEDA) | ⚠️ Manual (ASG) |
| **Deploy** | `aws ecs update-service` | `kubectl apply` | Scripts custom |
| **Custo operacional** | Baixo | Alto (cluster + nodes) | Médio |
| **Custo infra** | Médio | Alto (+$72/mês control plane) | Baixo |
| **Networking** | awsvpc (simples) | VPC CNI (complexo) | VPC direto |
| **Monitoring** | CloudWatch nativo | Prometheus + Grafana | Manual |
| **Rolling deploys** | ✅ Built-in | ✅ Built-in | ⚠️ Manual |
| **Drain de conexões** | ✅ Target group drain | ✅ Pod lifecycle hooks | ⚠️ Manual |
| **Learning curve** | Baixa | Alta | Média |
| **Quando usar** | 90% dos casos | +50 microservices, multi-cloud | Budget apertado |

---

## 💰 Estimativa de Custos (região us-east-1)

### Cenário: 5.000 jogadores simultâneos

| Serviço | Config | Custo/mês (estimado) |
|---------|--------|---------------------|
| **ALB** | 1 ALB + LCUs | ~$25 |
| **ECS Fargate** | 3 tasks × (2 vCPU, 4GB) | ~$220 |
| **ElastiCache** | Serverless (~2GB) | ~$90 |
| **RDS Aurora** | Serverless v2 (2-4 ACU) | ~$150 |
| **Data Transfer** | ~500GB/mês | ~$45 |
| **CloudWatch** | Logs + Metrics | ~$20 |
| **Route 53** | 1 hosted zone | ~$1 |
| **Total** | | **~$550/mês** |

### Se fosse EKS no mesmo cenário:

| Serviço | Config | Custo/mês (estimado) |
|---------|--------|---------------------|
| **EKS Control Plane** | 1 cluster | **$72** (custo fixo) |
| **EC2 Nodes** | 3 × m6i.large | ~$210 |
| **ALB** | via Ingress Controller | ~$25 |
| **ElastiCache** | mesma config | ~$90 |
| **RDS** | mesma config | ~$150 |
| **Total** | | **~$570/mês** |

> Custo similar, mas com **muito mais complexidade operacional** no EKS.

---

## 🚀 Caminho Evolutivo

```
Fase 1 — MVP (0 a 1.000 jogadores)
├── ECS Fargate: 2 tasks (1 vCPU, 2GB)
├── ElastiCache Serverless
├── RDS Aurora Serverless v2
└── Custo: ~$200/mês

Fase 2 — Growth (1.000 a 10.000 jogadores)
├── ECS Fargate: 3-5 tasks (2 vCPU, 4GB)
├── ElastiCache r7g.large (mais previsível)
├── RDS Aurora r6g.large
├── CloudFront para assets estáticos
└── Custo: ~$600/mês

Fase 3 — Scale (10.000+ jogadores)
├── Avaliar migração para EKS SE necessário
├── Múltiplos "mundos" = múltiplos clusters
├── Redis Cluster mode
├── RDS com read replicas
├── Considerar sharding por mundo
└── Custo: ~$1.500+/mês
```

> [!TIP]
> **Sharding natural do Tribal Wars**: Cada "Mundo" é independente. Jogadores do Mundo 1 nunca interagem com o Mundo 2. Isso significa que cada mundo pode rodar em seu próprio cluster/service, facilitando enormemente o scaling.

---

## ⚡ Config Específica para WebSocket no ALB

```json
// Target Group - configurações importantes
{
  "TargetType": "ip",
  "Protocol": "HTTP",
  "Port": 8080,
  "HealthCheckPath": "/health",
  "HealthCheckIntervalSeconds": 30,
  "DeregistrationDelay": 300,
  
  "Stickiness": {
    "Enabled": true,
    "Type": "app_cookie",
    "CookieName": "GAME_SESSION",
    "DurationSeconds": 86400
  },

  "Attributes": {
    "idle_timeout.timeout_seconds": 3600
  }
}
```

> [!IMPORTANT]
> **`DeregistrationDelay: 300`** (5 minutos) — quando fazer deploy de uma nova versão, o ALB espera 5 minutos antes de cortar conexões do container antigo. Isso dá tempo para os clientes reconectarem gracefully ao novo container.

---

## ✅ Resumo

| Pergunta | Resposta |
|----------|---------|
| EKS é o melhor? | **Não para começar.** ECS Fargate é mais simples e suficiente. |
| Quando migrar para EKS? | Quando tiver +50 serviços, multi-cloud, ou equipe DevOps dedicada. |
| Componente mais crítico? | **Redis** — é o backbone de comunicação entre instâncias. |
| Maior armadilha? | ALB idle timeout padrão (60s) — precisa subir para 3600s. |
| Scaling metric? | Número de **conexões ativas**, não CPU. |
