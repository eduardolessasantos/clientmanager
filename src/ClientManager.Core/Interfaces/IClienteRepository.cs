using ClientManager.Core.Entities;

namespace ClientManager.Core.Interfaces;

public interface IClienteRepository : IRepository<Cliente>
{
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Cliente cliente);
}
