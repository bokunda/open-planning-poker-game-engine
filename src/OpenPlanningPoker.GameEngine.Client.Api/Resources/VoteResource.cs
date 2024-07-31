namespace OpenPlanningPoker.GameEngine.Client.Api.Resources;

public class VoteResource(HttpClient httpClient) : IVoteResource
{
    private const string ControllerName = "votes";

    public async Task<ApiCollection<GetVotesItem>> GetTicketVotes(Guid ticketId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"{ControllerName}/{ticketId}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ApiCollection<GetVotesItem>>(cancellationToken))!;
    }

    public async Task<CreateVoteResponse> CreateVote(CreateVoteCommand data, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"{ControllerName}", data, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<CreateVoteResponse>(cancellationToken))!;
    }

    public async Task<UpdateVoteResponse> UpdateVote(UpdateVoteCommand data, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"{ControllerName}", data, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<UpdateVoteResponse>(cancellationToken))!;
    }
}