using ClientManager.Web.Models;
using ClientManager.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientManager.Web.Pages.Projects;

public class DetailsModel : PageModel
{
    private readonly ClientManagerApiService _apiService;

    public DetailsModel(ClientManagerApiService apiService)
    {
        _apiService = apiService;
    }

    public ProjectViewModel Project { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToPage("/Projects/Index");
        }

        var project = await _apiService.GetProjectByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        Project = project;
        return Page();
    }
}
