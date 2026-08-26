using ClientManager.Core.Entities;

namespace ClientManager.Core.Interfaces;

public interface IProjetoRepository : IRepository<Projeto>
{
    Task<IEnumerable<Projeto>> GetByClientIdAsync(Guid clientId);
    Task<Projeto?> GetWithPropostasAsync(Guid id);
    Task<IEnumerable<Projeto>> GetWithClienteECategoriaAsync();
}
