namespace ClientManager.Web.Models;

public class ProjectViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Em Andamento";
    public decimal Budget { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
}
