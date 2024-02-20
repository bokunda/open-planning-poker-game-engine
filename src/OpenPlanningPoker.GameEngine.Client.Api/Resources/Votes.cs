namespace OpenPlanningPoker.GameEngine.Client.Api.Resources;

public class Votes
{
    private readonly HttpClient _httpClient;
    private const string ControllerName = nameof(Votes);

    public Votes(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Returns votes for a ticket - {ticketId}
    /// </summary>
    /// <returns></returns>
    public async Task<GetVotesResponse> GetTicketVotes(Guid ticketId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{ControllerName}/{ticketId}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<GetVotesResponse>(cancellationToken))!;
    }

    /// <summary>
    /// Creates a vote
    /// </summary>
    /// <returns></returns>
    public async Task<CreateVoteResponse> CreateVote(CreateVoteCommand data, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerName}", data, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<CreateVoteResponse>(cancellationToken))!;
    }

    /// <summary>
    /// Updates a vote
    /// </summary>
    /// <returns></returns>
    public async Task<UpdateVoteResponse> UpdateVote(UpdateVoteCommand data, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"{ControllerName}", data, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<UpdateVoteResponse>(cancellationToken))!;
    }
}