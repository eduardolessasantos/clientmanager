using System.Configuration;

namespace ClientManager.Data;

public static class ConfigService
{
    public static string ModoAtual { get; private set; } = "SqliteLocal";

    public static string ObterConnectionString()
    {
        var connStr = ConfigurationManager.ConnectionStrings[ModoAtual]?.ConnectionString;
        if (string.IsNullOrEmpty(connStr))
        {
            return "Data Source=clientes.db";
        }
        return connStr;
    }

    public static void DefinirModo(string modo)
    {
        if (modo == "SqliteLocal" || modo == "SqlServerRede")
        {
            ModoAtual = modo;
        }
    }
}
