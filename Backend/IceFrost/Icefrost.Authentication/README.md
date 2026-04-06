# Icefrost.Authentication

Microservico responsavel pela autenticacao e gestao de utilizadores do ecossistema Ironfrost-Sagas.

## Responsabilidades

- **Autenticacao via JWT** - Gera tokens Bearer com validade de 30 minutos, contendo roles (Admin/User).
- **Registo de contas** - Criacao de novas contas com validacao de email, username e complexidade de password.
- **Login** - Autenticacao por email ou username com verificacao de password hashada.
- **Gestao de utilizadores (BackOffice)** - Endpoint administrativo para criacao de utilizadores.

## Endpoints

| Metodo | Rota | Descricao | Autorizacao |
|--------|------|-----------|-------------|
| POST | `/api/authentication/Login` | Login e obtencao de token JWT | Publico |
| POST | `/api/authentication/CreateAccount` | Registo de nova conta | Publico |
| POST | `/api/usercreator/CreateUser` | Criacao de utilizador (admin) | Admin |

## Stack

- .NET 9.0 / ASP.NET Core
- Entity Framework Core com PostgreSQL
- JWT Bearer Authentication
- Docker

## Seguranca

- Passwords hashadas com `PasswordHasher` do ASP.NET Identity (com salt)
- Validacao de formato de email
- Requisitos de complexidade de password (min. 8 caracteres, maiuscula, digito, caracter especial)
- Middleware de validacao de requests antes de atingir os controllers
- Role-Based Access Control (Admin / User)

## Entidades

- **Users** - id, username, email, password (hash), is_admin, created_at

## Configuracao

As configuracoes de JWT (Key, Issuer) e base de dados PostgreSQL sao lidas a partir de `appsettings.json`.
