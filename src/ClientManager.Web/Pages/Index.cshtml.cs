using ClientManager.Core.Entities;
using ClientManager.Web.Models;
using ClientManager.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientManager.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ClientManagerApiService _apiService;

    public IndexModel(ClientManagerApiService apiService)
    {
        _apiService = apiService;
    }

    public List<ProjectViewModel> RecentProjects { get; set; } = new();
    public int TotalClientsCount { get; set; }
    public int TotalProjectsCount { get; set; }

    public async Task OnGetAsync()
    {
        var clients = await _apiService.GetClientsAsync();
        var projects = await _apiService.GetProjectsAsync();

        TotalClientsCount = clients.Count;
        TotalProjectsCount = projects.Count;
        RecentProjects = projects.Take(3).ToList();
    }
}
