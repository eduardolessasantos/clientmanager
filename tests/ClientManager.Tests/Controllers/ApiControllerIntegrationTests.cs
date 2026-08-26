using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ClientManager.Api.DTOs;
using ClientManager.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ClientManager.Tests.Controllers;

public class ApiControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ApiControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ClientManagerDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ClientManagerDbContext>(options =>
                {
                    options.UseInMemoryDatabase("IntegrationTestDb_" + Guid.NewGuid());
                });
            });
        });
    }

    [Fact]
    public async Task PostLogin_ComCredenciaisValidas_DeveRetornar200OkEToken()
    {
        // Arrange
        var client = _factory.CreateClient();
        var loginDto = new LoginDto { Email = "admin@clientmanager.com", Senha = "123" };

        // Act
        var response = await client.PostAsJsonAsync("/api/auth/login", loginDto);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var tokenResult = await response.Content.ReadFromJsonAsync<TokenResultDto>();
        Assert.NotNull(tokenResult);
        Assert.False(string.IsNullOrEmpty(tokenResult.Token));
    }

    [Fact]
    public async Task GetClients_PermiteAcessoPublico_DeveRetornar200Ok()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/clients");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PostClient_SemTokenAutenticacao_DeveRetornar401Unauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var createDto = new CreateClienteDto
        {
            Nome = "Cliente Sem Token",
            Email = "semtoken@cliente.com",
            Telefone = "11988887777",
            CPF = "123.456.789-00",
            Endereco = "Rua Teste, 1"
        };

        // Act
        var response = await client.PostAsJsonAsync("/api/clients", createDto);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task PostClient_ComTokenAutenticado_DeveRetornar201Created()
    {
        // Arrange
        var client = _factory.CreateClient();

        // 1. Login to get JWT Token
        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new LoginDto { Email = "admin@test.com", Senha = "123" });
        var tokenResult = await loginResponse.Content.ReadFromJsonAsync<TokenResultDto>();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResult!.Token);

        var createDto = new CreateClienteDto
        {
            Nome = "Cliente Autenticado Teste",
            Email = "autenticado@cliente.com",
            Telefone = "11977776666",
            CPF = "987.654.321-10",
            RG = "12.345.678-9",
            CNH = "12345678900",
            Endereco = "Av Paulista, 1500",
            Cidade = "São Paulo",
            Estado = "SP"
        };

        // Act
        var response = await client.PostAsJsonAsync("/api/clients", createDto);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var createdCliente = await response.Content.ReadFromJsonAsync<ClienteDto>();
        Assert.NotNull(createdCliente);
        Assert.Equal("Cliente Autenticado Teste", createdCliente.Nome);
        Assert.Equal("987.654.321-10", createdCliente.CPF);
    }

    [Fact]
    public async Task GetProjects_PermiteAcessoPublico_DeveRetornar200Ok()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/projects");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetProjectsByClient_DeveRetornar200Ok()
    {
        // Arrange
        var client = _factory.CreateClient();
        var clientId = Guid.NewGuid();

        // Act
        var response = await client.GetAsync($"/api/clients/{clientId}/projects");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
