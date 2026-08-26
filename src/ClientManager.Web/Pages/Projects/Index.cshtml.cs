using ClientManager.Web.Models;
using ClientManager.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientManager.Web.Pages.Projects;

public class IndexModel : PageModel
{
    private readonly ClientManagerApiService _apiService;

    public IndexModel(ClientManagerApiService apiService)
    {
        _apiService = apiService;
    }

    public List<ProjectViewModel> Projects { get; set; } = new();
    public string? SelectedStatus { get; set; }

    public async Task OnGetAsync(string? status)
    {
        SelectedStatus = status;
        var allProjects = await _apiService.GetProjectsAsync();

        if (!string.IsNullOrEmpty(status))
        {
            Projects = allProjects.Where(p => p.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        else
        {
            Projects = allProjects;
        }
    }
}
