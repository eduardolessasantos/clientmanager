namespace ClientManager.Core.Entities;

public class Proposta
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProjetoId { get; set; }
    public decimal Valor { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string Status { get; set; } = "Enviada"; // Enviada, Aceita, Rejeitada, Cancelada
    public DateTime DataEnvio { get; set; } = DateTime.UtcNow;
    public DateTime DataValidade { get; set; } = DateTime.UtcNow.AddDays(30);
}
