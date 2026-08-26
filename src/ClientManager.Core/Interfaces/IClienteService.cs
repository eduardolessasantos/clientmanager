using ClientManager.Core.Entities;

namespace ClientManager.Core.Interfaces;

public interface IClienteService
{
    Task<Guid> CriarClienteAsync(Cliente cliente);
    Task EditarClienteAsync(Cliente cliente);
    Task ExcluirClienteAsync(Guid id);
    Task<Cliente?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Cliente>> ObterTodosAsync();
}
