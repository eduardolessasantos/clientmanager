namespace ClientManager.Core.Entities;

public class Cliente
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string? RG { get; set; }
    public string? CNH { get; set; }
    public string Endereco { get; set; } = string.Empty;
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? CEP { get; set; }
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    public List<Projeto> Projetos { get; set; } = new();
}
