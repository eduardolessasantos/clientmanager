# Instruções de Uso - ClientManager

Este documento contém os passos necessários para preparar o ambiente, clonar o repositório, executar a aplicação via Docker ou localmente, realizar testes e gerar pacotes de publicação.

---

## 1. Pré-requisitos

Certifique-se de ter os seguintes softwares instalados em seu ambiente:
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (com suporte a containers Linux)
- [Git](https://git-scm.com/)

---

## 2. Clonar o Repositório

Abra o terminal e execute o comando abaixo para clonar o repositório do projeto:

```bash
git clone https://github.com/usuario/ClientManager.git
cd ClientManager
```

---

## 3. Rodar com Docker (Recomendado)

Para subir todos os serviços containerizados (Web API, Frontend Razor Pages e Banco de Dados MySQL) em segundo plano, execute:

```bash
docker-compose up -d --build
```

> **Nota**: O container do banco de dados MySQL inicializará e executará automaticamente o script de criação de tabelas `sql/cria_banco.sql`.

Para verificar o status dos containers em execução:
```bash
docker-compose ps
```

Para parar os serviços:
```bash
docker-compose down
```

---

## 4. Acessar a Aplicação

Com os containers em execução, acesse os seguintes links no navegador:

- **API Swagger / OpenAPI**: [http://localhost:5000/swagger](http://localhost:5000/swagger)
- **Frontend Web (Razor Pages)**: [http://localhost:8080](http://localhost:8080)

---

## 5. Credenciais de Teste

Para realizar autenticação e testes na API ou no sistema, utilize as credenciais padrão de demonstração:

- **Usuário / E-mail**: `admin@exemplo.com`
- **Senha**: `Senha123`

### Como autenticar no Swagger UI:
1. Faça uma chamada ao endpoint `POST /api/auth/login` informando as credenciais acima.
2. Copie o valor da propriedade `token` retornada na resposta.
3. No topo da página do Swagger UI, clique no botão **Authorize**.
4. Digite `Bearer ` seguido do token copiado (exemplo: `Bearer eyJhbGciOi...`).
5. Clique em **Authorize** e feche o modal. Os endpoints protegidos por `[Authorize]` estarão liberados para execução.

---

## 6. Executar Testes

Para executar a suíte completa de testes unitários e de integração utilizando o `dotnet test`:

```bash
dotnet test ./tests/ClientManager.Tests
```

Para rodar todos os projetos de teste da solução:
```bash
dotnet test ClientManager.sln
```

---

## 7. Gerar Novo Build / Publicação

Para gerar os artefatos compilados e otimizados para produção em modo `Release`:

```bash
dotnet publish ClientManager.sln -c Release
```

Os artefatos compilados estarão disponíveis nas seguintes pastas:
- API: `src/ClientManager.Api/bin/Release/net8.0/publish/`
- Web Frontend: `src/ClientManager.Web/bin/Release/net8.0/publish/`
