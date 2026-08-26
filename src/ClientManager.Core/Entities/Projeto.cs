namespace ClientManager.Core.Entities;

public class Projeto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Status { get; set; } = "Em Andamento"; // Em Andamento, Concluído, Planejamento, Pausado
    public decimal Orcamento { get; set; }
    public DateTime DataInicio { get; set; } = DateTime.UtcNow;
    public DateTime? DataFim { get; set; }
    public Guid? ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    public Guid? CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
    public List<Proposta> Propostas { get; set; } = new();
}
