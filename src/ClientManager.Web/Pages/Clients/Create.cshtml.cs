using ClientManager.Web.Models;
using ClientManager.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientManager.Web.Pages.Clients;

public class CreateModel : PageModel
{
    private readonly ClientManagerApiService _apiService;

    public CreateModel(ClientManagerApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public ClientInputModel Input { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var sucesso = await _apiService.CreateClientAsync(Input);
        if (sucesso)
        {
            TempData["SuccessMessage"] = "Cliente cadastrado com sucesso!";
            return RedirectToPage("/Index");
        }

        ModelState.AddModelError(string.Empty, "Ocorreu um erro ao comunicar com a API. Tente novamente.");
        return Page();
    }
}
