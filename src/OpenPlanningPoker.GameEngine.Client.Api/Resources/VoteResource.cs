namespace OpenPlanningPoker.GameEngine.Client.Api.Resources;

public class VoteResource : IVoteResource
{
    private readonly HttpClient _httpClient;
    private const string ControllerName = "votes";

    public VoteResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiCollection<GetVotesItem>> GetTicketVotes(Guid ticketId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{ControllerName}/{ticketId}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ApiCollection<GetVotesItem>>(cancellationToken))!;
    }

    public async Task<CreateVoteResponse> CreateVote(CreateVoteCommand data, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerName}", data, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<CreateVoteResponse>(cancellationToken))!;
    }

    public async Task<UpdateVoteResponse> UpdateVote(UpdateVoteCommand data, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"{ControllerName}", data, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<UpdateVoteResponse>(cancellationToken))!;
    }
}