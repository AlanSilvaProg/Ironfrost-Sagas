# Ironfrost.GameService

Microservico responsavel pela logica de jogo do Ironfrost-Sagas, inspirado em Tribal Wars. Gere mundos, aldeias, edificios e tropas.

## Responsabilidades

- **Gestao de Mundos** - Criacao e configuracao de servidores/mundos com parametros como capacidade, bots, velocidade e condicao de vitoria.
- **Gestao de Aldeias** - Registo de aldeias com coordenadas, pontos, lealdade e associacao a jogadores/mundos.
- **Sistema de Edificios** - Controlo de niveis e upgrades de 15 tipos de edificios por aldeia (Quartel-General, Quartel, Estabulo, Oficina, Ferreiro, Mercado, Serracao, Mina de Barro, Mina de Ferro, Quinta, Armazem, Muralha, Praca de Reuniao, Igreja, Academia).
- **Sistema de Tropas** - Gestao de 11 tipos de tropas por aldeia (Lanceiro, Espadachim, Guerreiro com Machado, Arqueiro, Cavalaria Ligeira/Pesada, Arqueiro Montado, Ariete, Catapulta, Nobre, Batedor).

## Stack

- .NET 9.0 / ASP.NET Core
- Entity Framework Core com PostgreSQL
- Docker

## Entidades

- **World** - id, capacity, bot_count, wild_villages, server_type, win_condition, speed_modifier
- **Village** - id, world_id, owner_id, x, y, name, points, loyalty, timestamps
- **VillageBuilding** - village_id, building_type, level, upgrade_status, upgrade_completion_time
- **VillageTroop** - village_id, troop_type, quantity

## Estado Atual

A infraestrutura de dados (entidades, DbContext, migrations) esta implementada. Controllers e logica de negocio estao em desenvolvimento.

## Configuracao

Conexao a base de dados PostgreSQL configurada em `appsettings.json`.
