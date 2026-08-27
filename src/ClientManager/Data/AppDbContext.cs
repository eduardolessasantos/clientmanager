using ClientManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientManager.Data;

public class AppDbContext : DbContext
{
    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connStr = ConfigService.ObterConnectionString();
            if (ConfigService.ModoAtual == "SqlServerRede")
            {
                optionsBuilder.UseSqlServer(connStr);
            }
            else
            {
                optionsBuilder.UseSqlite(connStr);
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NomeCompleto).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Cpf).IsRequired().HasMaxLength(20);
            entity.HasIndex(e => e.Cpf);
            entity.HasIndex(e => e.NomeCompleto);
        });
    }
}
