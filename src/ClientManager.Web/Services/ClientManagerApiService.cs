using System.Net.Http.Json;
using ClientManager.Core.Entities;
using ClientManager.Web.Models;

namespace ClientManager.Web.Services;

public class ClientManagerApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ClientManagerApiService> _logger;

    public ClientManagerApiService(HttpClient httpClient, ILogger<ClientManagerApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<Client>> GetClientsAsync()
    {
        try
        {
            var clients = await _httpClient.GetFromJsonAsync<List<Client>>("api/clients");
            return clients ?? new List<Client>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter lista de clientes da API");
            return new List<Client>();
        }
    }

    public async Task<bool> CreateClientAsync(ClientInputModel model)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/clients", model);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao cadastrar cliente via API");
            return false;
        }
    }

    public async Task<List<ProjectViewModel>> GetProjectsAsync()
    {
        try
        {
            var projects = await _httpClient.GetFromJsonAsync<List<ProjectViewModel>>("api/projects");
            return projects ?? new List<ProjectViewModel>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter lista de projetos da API");
            return new List<ProjectViewModel>();
        }
    }

    public async Task<ProjectViewModel?> GetProjectByIdAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ProjectViewModel>($"api/projects/{id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter projeto com ID {ProjectId} da API", id);
            return null;
        }
    }
}
