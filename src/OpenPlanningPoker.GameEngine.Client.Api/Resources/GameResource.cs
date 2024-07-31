namespace OpenPlanningPoker.GameEngine.Client.Api.Resources;

public class GameResource(HttpClient httpClient) : IGameResource
{
    private const string ControllerNameGame = "games";
    private const string ControllerNameGamePlayer = "gameplayer";

    public async Task<GetGameResponse> GetGameDetails(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"{ControllerNameGame}/{id}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<GetGameResponse>(cancellationToken))!;
    }

    public async Task<CreateGameResponse> CreateGame(CreateGameCommand data, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(ControllerNameGame, data, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<CreateGameResponse>(cancellationToken))!;
    }

    public async Task<ApiCollection<ListPlayersItem>> GetParticipants(Guid gameId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"{ControllerNameGamePlayer}/{gameId}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ApiCollection<ListPlayersItem>>(cancellationToken))!;
    }

    /// <summary>
    /// Join Game - join/{gameId}
    /// </summary>
    public async Task<JoinGameResponse> JoinGame(Guid gameId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"{ControllerNameGame}/join/{gameId}", new {}, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<JoinGameResponse>(cancellationToken))!;
    }

    /// <summary>
    /// Leave a Game - leave/{gameId}
    /// </summary>
    public async Task<LeaveGameResponse> LeaveGame(Guid gameId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"{ControllerNameGame}/leave/{gameId}", new { }, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<LeaveGameResponse>(cancellationToken))!;
    }
}