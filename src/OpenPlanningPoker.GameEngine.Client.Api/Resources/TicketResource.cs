using OpenPlanningPoker.GameEngine.Api.Models;

namespace OpenPlanningPoker.GameEngine.Client.Api.Resources;

public class TicketResource : ITicketResource
{
    private readonly HttpClient _httpClient;
    private const string ControllerName = "tickets";

    public TicketResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetTicketResponse> GetTicket(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{ControllerName}/{id}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<GetTicketResponse>(cancellationToken))!;
    }

    public async Task<ApiCollection<GetTicketsItem>> GetTickets(Guid gameId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{ControllerName}/game/{gameId}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ApiCollection<GetTicketsItem>>(cancellationToken))!;
    }

    public async Task<CreateTicketResponse> CreateTicket(CreateTicketCommand data, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerName}", data, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<CreateTicketResponse>(cancellationToken))!;
    }

    public async Task<ApiCollection<ImportTicketItem>> ImportTicket(ImportTicketsCommand data, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerName}/import", data, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ApiCollection<ImportTicketItem>>(cancellationToken))!;
    }

    public async Task<ApiCollection<ImportTicketItem>> ImportTicketsCsv(Guid gameId, IFormFile file, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerName}/import/csv/{gameId}", file, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ApiCollection<ImportTicketItem>>(cancellationToken))!;
    }

    public async Task<DeleteTicketResponse> DeleteTicket(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync($"{ControllerName}/{id}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<DeleteTicketResponse>(cancellationToken))!;
    }
}