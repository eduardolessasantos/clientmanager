using ClientManager.Core.Entities;

namespace ClientManager.Core.Interfaces;

public interface IProjetoDomainRepository : IRepository<Projeto>
{
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Projeto projeto);
    Task AddPropostaAsync(Proposta proposta);
    Task<Proposta?> GetPropostaByIdAsync(Guid propostaId);
    Task UpdatePropostaAsync(Proposta proposta);
}
