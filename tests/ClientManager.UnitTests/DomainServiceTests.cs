using ClientManager.Core.CQRS.Clientes.Commands;
using ClientManager.Core.CQRS.Projetos.Commands;
using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;
using ClientManager.Core.Services;
using ClientManager.Core.Validations;
using ClientManager.Infrastructure.Data;
using ClientManager.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClientManager.UnitTests;

public class DomainServiceTests
{
    private readonly IServiceProvider _serviceProvider;

    public DomainServiceTests()
    {
        var services = new ServiceCollection();

        services.AddLogging();
        services.AddDbContext<ClientManagerDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IProjetoDomainRepository, ProjetoDomainRepository>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Cliente).Assembly));

        services.AddTransient<IClienteService, ClienteService>();
        services.AddTransient<IProjetoService, ProjetoService>();

        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public async Task ClienteService_CriarEObterCliente_DeveRetornarClienteCriado()
    {
        // Arrange
        var clienteService = _serviceProvider.GetRequiredService<IClienteService>();
        var cliente = new Cliente
        {
            Nome = "Empresa ABC",
            Email = "contato@empresaabc.com",
            Telefone = "11988887777",
            CPF = "123.456.789-90",
            RG = "12.345.678-9",
            CNH = "12345678900",
            Endereco = "Rua Principal, 500",
            Cidade = "São Paulo",
            Estado = "SP"
        };

        // Act
        var id = await clienteService.CriarClienteAsync(cliente);
        var resultado = await clienteService.ObterPorIdAsync(id);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Empresa ABC", resultado.Nome);
        Assert.Equal("123.456.789-90", resultado.CPF);
        Assert.Equal("Rua Principal, 500", resultado.Endereco);
    }

    [Fact]
    public async Task ProjetoService_CriarAssociarEProposta_DeveExecutarFluxoCompleto()
    {
        // Arrange
        var clienteService = _serviceProvider.GetRequiredService<IClienteService>();
        var projetoService = _serviceProvider.GetRequiredService<IProjetoService>();

        var clienteId = await clienteService.CriarClienteAsync(new Cliente
        {
            Nome = "Cliente Teste",
            Email = "teste@cliente.com",
            Telefone = "11977776666",
            CPF = "987.654.321-00",
            Endereco = "Av Paulista, 1000"
        });

        var projetoId = await projetoService.CriarProjetoAsync(new Projeto
        {
            Titulo = "Desenvolvimento de App Mobile",
            Descricao = "App nativo iOS/Android para vendas externas",
            Orcamento = 50000.00m,
            DataInicio = DateTime.UtcNow
        });

        // Act 1: Associar Cliente ao Projeto
        await projetoService.AssociarClienteAsync(projetoId, clienteId);

        // Act 2: Enviar Proposta
        var propostaId = await projetoService.EnviarPropostaAsync(new Proposta
        {
            ProjetoId = projetoId,
            Valor = 48000.00m,
            Descricao = "Proposta comercial inicial com 5% de desconto à vista",
            DataValidade = DateTime.UtcNow.AddDays(15)
        });

        // Act 3: Mudar Status da Proposta para Aceita
        await projetoService.MudarStatusPropostaAsync(propostaId, "Aceita");

        // Assert
        var projeto = await projetoService.ObterPorIdAsync(projetoId);
        Assert.NotNull(projeto);
        Assert.Equal(clienteId, projeto.ClienteId);
        Assert.Single(projeto.Propostas);
        Assert.Equal("Aceita", projeto.Propostas.First().Status);
    }

    [Fact]
    public void FluentValidation_CreateClienteValidator_DeveValidarCamposObrigatorios()
    {
        // Arrange
        var validator = new CreateClienteCommandValidator();
        var commandInvalido = new CreateClienteCommand("", "email-invalido", "", "", null, null, "", null, null, null, null);

        // Act
        var result = validator.Validate(commandInvalido);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Nome");
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
        Assert.Contains(result.Errors, e => e.PropertyName == "Telefone");
        Assert.Contains(result.Errors, e => e.PropertyName == "CPF");
        Assert.Contains(result.Errors, e => e.PropertyName == "Endereco");
    }

    [Fact]
    public void FluentValidation_CreateProjetoValidator_DeveValidarOrcamento()
    {
        // Arrange
        var validator = new CreateProjetoCommandValidator();
        var commandInvalido = new CreateProjetoCommand("Projeto Sem Orcamento", "Descricao", 0, DateTime.UtcNow, null, null, null);

        // Act
        var result = validator.Validate(commandInvalido);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Orcamento");
    }
}
