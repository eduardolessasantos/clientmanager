using ClientManager.Core.Entities;
using ClientManager.Infrastructure.Repositories;
using Xunit;

namespace ClientManager.UnitTests;

public class ClientRepositoryTests
{
    [Fact]
    public async Task AddAsync_ShouldAddClientToRepository()
    {
        // Arrange
        var repository = new ClientRepository();
        var client = new Client
        {
            Name = "João Silva",
            Email = "joao@example.com",
            Phone = "11999998888"
        };

        // Act
        await repository.AddAsync(client);
        var result = await repository.GetByIdAsync(client.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("João Silva", result.Name);
        Assert.Equal("joao@example.com", result.Email);
    }
}
