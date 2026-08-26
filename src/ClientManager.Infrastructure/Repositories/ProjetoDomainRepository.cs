using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;
using ClientManager.Infrastructure.Data;

namespace ClientManager.Infrastructure.Repositories;

public class ProjetoDomainRepository : Repository<Projeto>, IProjetoDomainRepository
{
    public ProjetoDomainRepository(ClientManagerDbContext context) : base(context)
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

    public async Task UpdateAsync(Projeto projeto)
    {
        Update(projeto);
        await SaveChangesAsync();
    }

    public async Task AddPropostaAsync(Proposta proposta)
    {
        await _context.Propostas.AddAsync(proposta);
        await SaveChangesAsync();
    }

    public async Task<Proposta?> GetPropostaByIdAsync(Guid propostaId)
    {
        return await _context.Propostas.FindAsync(propostaId);
    }

    public async Task UpdatePropostaAsync(Proposta proposta)
    {
        _context.Propostas.Update(proposta);
        await SaveChangesAsync();
    }
}
