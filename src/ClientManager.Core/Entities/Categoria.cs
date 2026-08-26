namespace ClientManager.Core.Entities;

public class Categoria
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public List<Projeto> Projetos { get; set; } = new();
}
