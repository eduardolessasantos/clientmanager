# ClientManager

Sistema de Gestão de Clientes e Projetos desenvolvido em **.NET 8** com arquitetura em camadas, CQRS (**MediatR**), **Entity Framework Core 8** (MySQL), **Razor Pages** (Bootstrap 5), **AutoMapper**, autenticação **JWT Bearer**, **Swagger/OpenAPI**, suíte de testes (**xUnit**, **Moq**, **WebApplicationFactory**) e pipeline CI com **GitHub Actions**.

---

## 🏗️ Estrutura do Projeto

```
ClientManager/
├── .github/workflows/ci.yml       # Pipeline CI (GitHub Actions)
├── docker-compose.yml             # Orquestração Docker (API, Web, MySQL)
├── sql/cria_banco.sql             # Script DDL de criação do banco MySQL
├── src/
│   ├── ClientManager.Api/         # Web API RESTful (Controllers, DTOs, JWT, Swagger)
│   ├── ClientManager.Core/        # Domínio, Entidades, Serviços, CQRS (MediatR), Validations
│   ├── ClientManager.Infrastructure/ # EF Core 8 DbContext, Migrações e Repositórios
│   └── ClientManager.Web/         # Frontend ASP.NET Core Razor Pages + Bootstrap 5
└── tests/
    ├── ClientManager.UnitTests/   # Testes unitários do domínio
    └── ClientManager.Tests/       # Suíte completa de testes (xUnit, Moq, WebApplicationFactory)
```

---

## 🐳 Execução via Docker Compose (Recomendado)

Suba toda a infraestrutura (API, Frontend e Banco MySQL) com um único comando:

```bash
docker-compose up -d --build
```

### 📍 Endpoints e Portas:
- **Frontend Web**: [http://localhost:8080](http://localhost:8080)
- **Web API RESTful**: [http://localhost:5000](http://localhost:5000)
- **Documentação Swagger UI**: [http://localhost:5000/swagger](http://localhost:5000/swagger)
- **Banco de Dados MySQL**: `localhost:3306` (User: `root`, Password: `270523`, Database: `clientmanager_db`)

Para encerrar os containers:
```bash
docker-compose down
```

---

## 💻 Execução Local sem Docker

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- MySQL Server rodando localmente (Usuário: `root`, Senha: `270523`)

### 1. Compilação
```bash
dotnet build ClientManager.sln
```

### 2. Execução dos Testes Unitários e de Integração
```bash
dotnet test
```

### 3. Execução da API
```bash
dotnet run --project src/ClientManager.Api
```
Acesse o Swagger em `https://localhost:7080/swagger`.

### 4. Execução da Aplicação Web
```bash
dotnet run --project src/ClientManager.Web
```
Acesse no navegador em `https://localhost:7196`.

---

## 🔑 Autenticação e Testes da API (JWT Bearer)

1. Faça uma requisição `POST /api/auth/login` na API/Swagger com qualquer e-mail e senha.
2. Copie o token de retorno (`TokenResultDto.Token`).
3. No Swagger UI, clique em **Authorize** e informe `Bearer {seu_token}`.
4. Execute requisições `POST`, `PUT` e `DELETE` em `/api/clients` e `/api/projects`.
