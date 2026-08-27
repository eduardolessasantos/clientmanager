using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClientManager.Data;
using ClientManager.Models;

namespace ClientManager.Services;

public class ImportResult
{
    public int SucessoCount { get; set; }
    public int ErrosCount { get; set; }
    public List<string> LogsErros { get; set; } = new();
}

public class ImportService
{
    /// <summary>
    /// Lê um arquivo CSV ou texto contendo dados legados (XP/Access/DBF exportado) e importa em lotes para alta performance.
    /// </summary>
    public async Task<ImportResult> ImportarDeCsvAsync(string caminhoArquivo, IProgress<int>? progress = null)
    {
        var result = new ImportResult();
        if (!File.Exists(caminhoArquivo))
        {
            result.LogsErros.Add("Arquivo não encontrado.");
            return result;
        }

        var linhas = await File.ReadAllLinesAsync(caminhoArquivo);
        if (linhas.Length == 0) return result;

        var clientesLote = new List<Cliente>();
        var totalLinhas = linhas.Length;
        var headerProcessado = false;

        for (int index = 0; index < linhas.Length; index++)
        {
            var linha = linhas[index];
            if (string.IsNullOrWhiteSpace(linha)) continue;

            if (!headerProcessado && (linha.Contains("Nome", StringComparison.OrdinalIgnoreCase) || linha.Contains("CPF", StringComparison.OrdinalIgnoreCase)))
            {
                headerProcessado = true;
                continue;
            }

            var partes = linha.Split(new[] { ',', ';', '\t' }, StringSplitOptions.None);
            if (partes.Length < 2)
            {
                result.ErrosCount++;
                result.LogsErros.Add($"Linha {index + 1}: Formato inválido.");
                continue;
            }

            var cliente = new Cliente
            {
                NomeCompleto = partes[0].Trim(),
                Cpf = partes.Length > 1 ? partes[1].Trim() : "",
                Rg = partes.Length > 2 ? partes[2].Trim() : "",
                Cnh = partes.Length > 3 ? partes[3].Trim() : "",
                CnhCategoria = partes.Length > 4 ? partes[4].Trim() : "",
                Logradouro = partes.Length > 5 ? partes[5].Trim() : "",
                Numero = partes.Length > 6 ? partes[6].Trim() : "",
                Bairro = partes.Length > 7 ? partes[7].Trim() : "",
                Cidade = partes.Length > 8 ? partes[8].Trim() : "",
                Estado = partes.Length > 9 ? partes[9].Trim() : "",
                Cep = partes.Length > 10 ? partes[10].Trim() : "",
                Telefone = partes.Length > 11 ? partes[11].Trim() : "",
                Celular = partes.Length > 12 ? partes[12].Trim() : "",
                DataCadastro = DateTime.Now
            };

            if (string.IsNullOrWhiteSpace(cliente.NomeCompleto))
            {
                result.ErrosCount++;
                result.LogsErros.Add($"Linha {index + 1}: Nome do cliente em branco.");
                continue;
            }

            clientesLote.Add(cliente);
            result.SucessoCount++;

            if (clientesLote.Count >= 500)
            {
                await SalvarLoteAsync(clientesLote);
                clientesLote.Clear();
            }

            int percentual = (int)((double)(index + 1) / totalLinhas * 100);
            progress?.Report(percentual);
        }

        if (clientesLote.Any())
        {
            await SalvarLoteAsync(clientesLote);
        }

        progress?.Report(100);
        return result;
    }

    private static async Task SalvarLoteAsync(List<Cliente> clientes)
    {
        using var context = new AppDbContext();
        await context.Clientes.AddRangeAsync(clientes);
        await context.SaveChangesAsync();
    }
}
