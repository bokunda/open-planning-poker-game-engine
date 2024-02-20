namespace OpenPlanningPoker.GameEngine.Client.Api.Resources;

public class Tickets
{
    private readonly HttpClient _httpClient;
    private const string ControllerName = nameof(Tickets);

    public Tickets(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Returns Ticket details - {id}
    /// </summary>
    /// <returns></returns>
    public async Task<GetTicketResponse> GetTicket(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{ControllerName}/{id}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<GetTicketResponse>(cancellationToken))!;
    }

    /// <summary>
    /// Returns Tickets for selected game - game/{gameId}
    /// </summary>
    /// <returns></returns>
    public async Task<GetTicketsResponse> GetTickets(Guid gameId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{ControllerName}/game/{gameId}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<GetTicketsResponse>(cancellationToken))!;
    }

    /// <summary>
    /// Creates a Ticket
    /// </summary>
    /// <returns></returns>
    public async Task<CreateTicketResponse> CreateTicket(CreateTicketCommand data, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerName}", data, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<CreateTicketResponse>(cancellationToken))!;
    }

    /// <summary>
    /// Import a list of Tickets using web API - import
    /// </summary>
    /// <returns></returns>
    public async Task<ImportTicketsResponse> ImportTicket(ImportTicketsCommand data, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerName}/import", data, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ImportTicketsResponse>(cancellationToken))!;
    }

    /// <summary>
    /// Import a list of Tickets using a CSV file - import/csv/{gameId}
    /// </summary>
    /// <returns></returns>
    public async Task<ImportTicketsResponse> ImportTicketsCsv(Guid gameId, IFormFile file, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerName}/import/csv/{gameId}", file, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ImportTicketsResponse>(cancellationToken))!;
    }

    /// <summary>
    /// Delete a Ticket - {id}
    /// </summary>
    /// <returns></returns>
    public async Task<DeleteTicketResponse> DeleteTicket(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync($"{ControllerName}/{id}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<DeleteTicketResponse>(cancellationToken))!;
    }
}