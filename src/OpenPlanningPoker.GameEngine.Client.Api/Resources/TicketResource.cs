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

    public async Task<GetTicketsResponse> GetTickets(Guid gameId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{ControllerName}/game/{gameId}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<GetTicketsResponse>(cancellationToken))!;
    }

    public async Task<CreateTicketResponse> CreateTicket(CreateTicketCommand data, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerName}", data, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<CreateTicketResponse>(cancellationToken))!;
    }

    public async Task<ImportTicketsResponse> ImportTicket(ImportTicketsCommand data, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerName}/import", data, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ImportTicketsResponse>(cancellationToken))!;
    }

    public async Task<ImportTicketsResponse> ImportTicketsCsv(Guid gameId, IFormFile file, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerName}/import/csv/{gameId}", file, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ImportTicketsResponse>(cancellationToken))!;
    }

    public async Task<DeleteTicketResponse> DeleteTicket(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync($"{ControllerName}/{id}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<DeleteTicketResponse>(cancellationToken))!;
    }
}