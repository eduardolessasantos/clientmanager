using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;
using ClientManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClientManager.Infrastructure.Repositories;

public class ProjetoRepository : Repository<Projeto>, IProjetoRepository
{
    public ProjetoRepository(ClientManagerDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Projeto>> GetByClientIdAsync(Guid clientId)
    {
        return await _dbSet.Where(p => p.ClienteId == clientId).ToListAsync();
    }

    public async Task<Projeto?> GetWithPropostasAsync(Guid id)
    {
        return await _dbSet.Include(p => p.Propostas)
                           .Include(p => p.Cliente)
                           .Include(p => p.Categoria)
                           .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Projeto>> GetWithClienteECategoriaAsync()
    {
        return await _dbSet.Include(p => p.Cliente)
                           .Include(p => p.Categoria)
                           .ToListAsync();
    }
}
