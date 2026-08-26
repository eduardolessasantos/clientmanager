using ClientManager.Core.Entities;

namespace ClientManager.Core.Interfaces;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(Guid id);
    Task<IEnumerable<Project>> GetByClientIdAsync(Guid clientId);
    Task AddAsync(Project project);
}
