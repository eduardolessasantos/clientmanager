using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;
using ClientManager.Infrastructure.Data;

namespace ClientManager.Infrastructure.Repositories;

public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    public ClienteRepository(ClientManagerDbContext context) : base(context)
    {
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            Remove(entity);
            await SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(Cliente cliente)
    {
        Update(cliente);
        await SaveChangesAsync();
    }
}
