using System;
using System.Linq;

namespace ClientManager.Models;

public class Cliente
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string NomeCompleto { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Rg { get; set; } = string.Empty;
    public string Cnh { get; set; } = string.Empty;
    public string CnhCategoria { get; set; } = string.Empty;
    public DateTime? CnhValidade { get; set; }

    // Endereço Completo
    public string Cep { get; set; } = string.Empty;
    public string Logradouro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;

    // Contato
    public string Telefone { get; set; } = string.Empty;
    public string Celular { get; set; } = string.Empty;

    public DateTime DataCadastro { get; set; } = DateTime.Now;

    public static bool ValidarCPF(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        var numeros = new string(cpf.Where(char.IsDigit).ToArray());
        if (numeros.Length != 11) return false;
        if (numeros.Distinct().Count() == 1) return false;

        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCpf = numeros.Substring(0, 9);
        var soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        var resto = soma % 11;
        var digito = resto < 2 ? 0 : 11 - resto;

        tempCpf += digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        var digito2 = resto < 2 ? 0 : 11 - resto;

        return numeros.EndsWith(digito.ToString() + digito2.ToString());
    }
}
