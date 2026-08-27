========================================================================
  CLIENTMANAGER - SISTEMA DE CADASTRO E MIGRAÇÃO OFFLINE DE CLIENTES
========================================================================

COMO EXECUTAR A APLICAÇÃO:
--------------------------
1. Execute o arquivo "ClientManager.exe" dentro da pasta "publish".
2. O sistema iniciará automaticamente no MODO DEMO usando SQLite local (arquivo "clientes.db").
3. Não é necessária conexão com a internet.

COMO CONFIGURAR O BANCO DE DADOS COMPARTILHADO EM REDE LOCAL (OPCIONAL):
-------------------------------------------------------------------------
Caso deseje utilizar o sistema com MÚLTIPLAS MÁQUINAS acessando o mesmo banco em servidor local:

1. Instale o SQL Server Express (Gratuito) no computador que servirá como servidor local.
2. Abra o arquivo "ClientManager.dll.config" em um editor de texto (Notepad).
3. Na seção <connectionStrings>, altere o parâmetro "SqlServerRede":
   Server=IP_OU_NOME_DO_SERVIDOR\SQLEXPRESS;Database=ClientManager;...
4. No sistema, selecione no menu superior "Modo Banco: SQL Server (Rede)".

MIGRAÇÃO DA BASE DE DADOS ANTIGA (~9.000 CLIENTES DO WINDOWS XP):
-----------------------------------------------------------------
1. Clique no botão "📥 Importar Base Antiga" na tela principal.
2. Selecione o arquivo CSV/Texto exportado do sistema antigo.
3. O sistema importará os 9.000 clientes com barra de progresso em lote.
