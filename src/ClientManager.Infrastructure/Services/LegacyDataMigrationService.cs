using ClientManager.Core.Entities;
using ClientManager.Infrastructure.Data;

namespace ClientManager.Infrastructure.Services;

public class LegacyDataMigrationService
{
    private readonly ClientManagerDbContext _context;

    public LegacyDataMigrationService(ClientManagerDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Importa clientes do banco legado de forma extremamente eficiente usando processamento em lote (Batch size: 1000).
    /// </summary>
    public async Task<int> ImportarClientesLegadoAsync(IEnumerable<Cliente> clientesLegado, int batchSize = 1000)
    {
        var totalImportados = 0;
        var lista = clientesLegado.ToList();

        for (int i = 0; i < lista.Count; i += batchSize)
        {
            var lote = lista.Skip(i).Take(batchSize);
            await _context.Clientes.AddRangeAsync(lote);
            totalImportados += await _context.SaveChangesAsync();
        }

        return totalImportados;
    }
}
