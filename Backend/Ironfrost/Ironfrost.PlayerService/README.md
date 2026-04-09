# Ironfrost.PlayerService

Microservico responsavel pela gestao de jogadores e grupos (Aetts) no ecossistema Ironfrost-Sagas.

## Responsabilidades

- **Gestao de Jogadores** - Perfis de jogadores com associacao a mundos, emblemas, pontuacao e contagem de aldeias.
- **Gestao de Aetts (Grupos/Faccoes)** - Criacao e gestao de grupos de jogadores com lider, emblema e pontuacao coletiva.

## Stack

- .NET 9.0 / ASP.NET Core
- Entity Framework Core com PostgreSQL
- Docker

## Entidades

### Players
| Campo | Tipo | Descricao |
|-------|------|-----------|
| id | UUID | Chave primaria |
| world_id | UUID? | Referencia ao mundo |
| username | string | Nome do jogador |
| aett_id | UUID? | Referencia ao grupo |
| emblem_index | byte | Indice do emblema visual |
| total_points | uint | Pontos acumulados |
| village_count | ushort | Numero de aldeias |
| created_at | DateTime | Data de criacao |

### Aetts (Grupos)
| Campo | Tipo | Descricao |
|-------|------|-----------|
| id | UUID | Chave primaria |
| world_id | UUID | Referencia ao mundo |
| aett_name | string | Nome do grupo |
| leader_id | UUID | Lider do grupo |
| emblem_index | byte | Indice do emblema |
| member_count | ushort | Numero de membros |
| total_points | uint | Pontos do grupo |
| created_at | DateTime | Data de criacao |

## Estado Atual

A infraestrutura de dados (entidades, DbContext, migrations) esta implementada. Controllers e logica de negocio estao em desenvolvimento.

## Configuracao

Conexao a base de dados PostgreSQL configurada em `appsettings.json`.
