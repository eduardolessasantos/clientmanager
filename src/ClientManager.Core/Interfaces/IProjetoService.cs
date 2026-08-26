using ClientManager.Core.Entities;

namespace ClientManager.Core.Interfaces;

public interface IProjetoService
{
    Task<Guid> CriarProjetoAsync(Projeto projeto);
    Task EditarProjetoAsync(Projeto projeto);
    Task ExcluirProjetoAsync(Guid id);
    Task AssociarClienteAsync(Guid projetoId, Guid clienteId);
    Task<Guid> EnviarPropostaAsync(Proposta proposta);
    Task MudarStatusPropostaAsync(Guid propostaId, string novoStatus);
    Task<Projeto?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Projeto>> ObterTodosAsync();
}
