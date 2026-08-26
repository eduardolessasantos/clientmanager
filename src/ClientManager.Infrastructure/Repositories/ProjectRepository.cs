using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;

namespace ClientManager.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly List<Project> _projects = new();

    public ProjectRepository()
    {
        // Seed initial sample projects
        var sampleClientId1 = Guid.NewGuid();
        var sampleClientId2 = Guid.NewGuid();

        _projects.AddRange(new[]
        {
            new Project
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Title = "Redesenho de Portal E-commerce",
                Description = "Reformulação completa da experiência do usuário e checkout integrado com pagamentos via PIX e Cartão.",
                Status = "Em Andamento",
                Budget = 45000.00m,
                StartDate = DateTime.UtcNow.AddMonths(-2),
                EndDate = DateTime.UtcNow.AddMonths(2),
                ClientId = sampleClientId1,
                ClientName = "TechCorp Soluções"
            },
            new Project
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Title = "Aplicativo Mobile de Gestão de Estoque",
                Description = "Desenvolvimento de aplicativo iOS/Android para controle de inventário em tempo real com leitor de código de barras.",
                Status = "Planejamento",
                Budget = 32000.00m,
                StartDate = DateTime.UtcNow.AddDays(-15),
                EndDate = DateTime.UtcNow.AddMonths(4),
                ClientId = sampleClientId2,
                ClientName = "Logística Express"
            },
            new Project
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Title = "Migração para Nuvem AWS",
                Description = "Migração da infraestrutura legada local para a AWS utilizando contêineres Docker e Kubernetes.",
                Status = "Concluído",
                Budget = 60000.00m,
                StartDate = DateTime.UtcNow.AddMonths(-6),
                EndDate = DateTime.UtcNow.AddMonths(-1),
                ClientId = sampleClientId1,
                ClientName = "TechCorp Soluções"
            }
        });
    }

    public Task<IEnumerable<Project>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Project>>(_projects);
    }

    public Task<Project?> GetByIdAsync(Guid id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(project);
    }

    public Task<IEnumerable<Project>> GetByClientIdAsync(Guid clientId)
    {
        var projects = _projects.Where(p => p.ClientId == clientId);
        return Task.FromResult(projects);
    }

    public Task AddAsync(Project project)
    {
        _projects.Add(project);
        return Task.CompletedTask;
    }
}
