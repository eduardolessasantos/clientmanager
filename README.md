# ClientManager

Sistema Desktop Windows (.NET 8 WPF) para **Cadastro de Clientes Offline** e **Migração de Dados Legados (~9.000 clientes)**.

---

## 🌐 Apresentação Interativa no GitHub Pages

A apresentação interativa do sistema está disponível via GitHub Pages. O repositório contém a página de apresentação em **`index.html`** e o workflow automático do GitHub Actions em **`.github/workflows/pages.yml`**.

---

## 🎯 Principais Funcionalidades

1. **Funcionamento 100% Offline**: Sem dependência de conexão com a internet.
2. **Duplo Modo de Banco de Dados**:
   - **Modo DEMO (SQLite)**: Inicializa instantaneamente usando o banco local `clientes.db` (zero configuração).
   - **Modo PRODUÇÃO (SQL Server Express)**: Permite o acesso compartilhado de múltiplas máquinas em rede local apontando para o servidor da empresa.
3. **Cadastro Completo de Clientes**:
   - Dados Pessoais: Nome Completo, CPF (com validação de dígitos verificadores), RG, CNH, Categoria CNH, Validade CNH.
   - Endereço Completo: Logradouro, Número, Bairro, Cidade, Estado (UF), CEP.
   - Contato: Telefone e Celular / WhatsApp.
4. **Migração em Lote da Base Antiga**:
   - Importação de arquivos `.csv` / `.txt` exportados de sistemas antigos (Windows XP / Access / DBF).
   - Processamento de alta performance em lote com barra de progresso em tempo real.
5. **Busca Rápida Instantânea**:
   - Filtro em tempo real por Nome ou CPF otimizado com índices no banco de dados.

---

## 📁 Estrutura do Repositório

```
ClientManager/
├── .github/
│   └── workflows/
│       ├── ci.yml                # Workflow de build e teste .NET
│       └── pages.yml             # Workflow de deploy automático do GitHub Pages
├── src/
│   └── ClientManager/            # Aplicação Desktop WPF (.NET 8)
│       ├── Models/
│       │   └── Cliente.cs        # Entidade Cliente com validação de CPF
│       ├── Data/
│       │   ├── AppDbContext.cs   # Contexto do EF Core (SQLite / SQL Server)
│       │   ├── ConfigService.cs  # Alternância de conexão entre os modos de banco
│       │   └── DbInitializer.cs  # Criação automática de tabelas e dados iniciais
│       ├── Services/
│       │   └── ImportService.cs  # Migração em lote de arquivos da base antiga
│       ├── Views/
│       │   ├── MainWindow.xaml   # Tela principal (DataGrid, Busca, Botões, Status)
│       │   └── ClienteFormWindow.xaml # Modal de cadastro e edição
│       └── App.config            # Configuração de ConnectionStrings
├── index.html                    # Página de apresentação técnica interativa
├── publish.bat                   # Script para compilação e publicação automática
├── README_CLIENTE.txt            # Guia simplificado para o cliente final
└── README.md                     # Documentação técnica do repositório
```

---

## 🚀 Como Executar o Projeto

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### 1. Execução via Código Fonte (Desenvolvimento)

Abra o terminal na pasta raiz do repositório e execute:

```bash
dotnet run --project src/ClientManager/ClientManager.csproj
```

---

### 2. Geração da Versão Final para o Cliente (Publish)

Para gerar a pasta executável independente para entrega ao cliente:

1. Dê um duplo clique no arquivo **`publish.bat`** ou execute no terminal:
   ```bash
   dotnet publish src/ClientManager/ClientManager.csproj -c Release -r win-x64 --self-contained false -o ./publish
   ```
2. A pasta **`publish`** será gerada na raiz contendo o **`ClientManager.exe`**.
3. Copie a pasta `publish` para o computador ou servidor do cliente.
