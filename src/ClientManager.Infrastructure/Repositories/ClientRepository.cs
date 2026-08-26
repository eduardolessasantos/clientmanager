using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;

namespace ClientManager.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly List<Client> _clients = new();

    public Task<IEnumerable<Client>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Client>>(_clients);
    }

    public Task<Client?> GetByIdAsync(Guid id)
    {
        var client = _clients.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(client);
    }

    public Task AddAsync(Client client)
    {
        _clients.Add(client);
        return Task.CompletedTask;
    }
}
