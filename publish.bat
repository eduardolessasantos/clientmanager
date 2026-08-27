@echo off
echo =======================================================
echo   GERANDO PUBLICAÇÃO ENXUTA DO CLIENTMANAGER (WIN-X64)
echo =======================================================
echo.

dotnet publish src/ClientManager/ClientManager.csproj -c Release -r win-x64 --self-contained false -o ./publish

echo.
echo =======================================================
echo   PUBLICAÇÃO CONCLUÍDA COM SUCESSO!
echo   Pasta de saída: ./publish
echo =======================================================
pause
