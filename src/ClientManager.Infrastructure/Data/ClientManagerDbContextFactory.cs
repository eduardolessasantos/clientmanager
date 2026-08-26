using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ClientManager.Infrastructure.Data;

public class ClientManagerDbContextFactory : IDesignTimeDbContextFactory<ClientManagerDbContext>
{
    public ClientManagerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ClientManagerDbContext>();
        var connectionString = "Server=localhost;Database=clientmanager_db;User=root;Password=270523;";

        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return new ClientManagerDbContext(optionsBuilder.Options);
    }
}
