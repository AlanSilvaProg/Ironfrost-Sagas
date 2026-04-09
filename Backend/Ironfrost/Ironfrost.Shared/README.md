# Ironfrost.Shared

Biblioteca partilhada (.NET 9.0 Class Library) que contem modelos e utilitarios comuns utilizados pelos microservicos do Ironfrost-Sagas.

## Responsabilidades

- Fornecer modelos de resposta padronizados para comunicacao entre servicos e respostas de API.
- Centralizar tipos e contratos reutilizaveis, evitando duplicacao entre microservicos.

## Modelos

### Response

```csharp
public record struct Response(bool ResponseResult, string Content, string ResponseMessage);
```

| Campo | Tipo | Descricao |
|-------|------|-----------|
| ResponseResult | bool | Indica sucesso ou falha da operacao |
| Content | string | Payload da resposta (ex: token JWT) |
| ResponseMessage | string | Mensagem descritiva do resultado |

## Utilizado por

- `Ironfrost.GameService`
- `Ironfrost.PlayerService`

## Stack

- .NET 9.0 Class Library
- Sem dependencias externas
