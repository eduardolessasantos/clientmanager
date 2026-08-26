using ClientManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientManager.Infrastructure.Data;

public class ClientManagerDbContext : DbContext
{
    public ClientManagerDbContext(DbContextOptions<ClientManagerDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Projeto> Projetos => Set<Projeto>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Proposta> Propostas => Set<Proposta>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Cliente configuration
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Telefone).IsRequired().HasMaxLength(20);
            entity.Property(e => e.CPF).IsRequired().HasMaxLength(20);
            entity.Property(e => e.RG).HasMaxLength(20);
            entity.Property(e => e.CNH).HasMaxLength(20);
            entity.Property(e => e.Endereco).IsRequired().HasMaxLength(250);
            entity.Property(e => e.Bairro).HasMaxLength(100);
            entity.Property(e => e.Cidade).HasMaxLength(100);
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.CEP).HasMaxLength(20);
        });

        // Categoria configuration
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Descricao).HasMaxLength(250);
        });

        // Projeto configuration
        modelBuilder.Entity<Projeto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Orcamento).HasPrecision(18, 2);

            entity.HasOne(e => e.Cliente)
                  .WithMany(c => c.Projetos)
                  .HasForeignKey(e => e.ClienteId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.Categoria)
                  .WithMany(c => c.Projetos)
                  .HasForeignKey(e => e.CategoriaId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Proposta configuration
        modelBuilder.Entity<Proposta>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Valor).HasPrecision(18, 2);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);

            entity.HasOne<Projeto>()
                  .WithMany(p => p.Propostas)
                  .HasForeignKey(e => e.ProjetoId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Usuario configuration
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.SenhaHash).IsRequired().HasMaxLength(250);
            entity.Property(e => e.Perfil).IsRequired().HasMaxLength(50);
        });
    }
}
