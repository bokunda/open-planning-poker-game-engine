namespace OpenPlanningPoker.GameEngine.Client.Api.Resources;

public class Games
{
    private readonly HttpClient _httpClient;

    private const string ControllerNameGame = nameof(Games);
    private const string ControllerNameGamePlayer = nameof(Games);

    public Games(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Returns game details - {id}
    /// </summary>
    /// <returns></returns>
    public async Task<GetGameResponse> GetGameDetails(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{ControllerNameGame}/{id}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<GetGameResponse>(cancellationToken))!;
    }

    /// <summary>
    /// Creates a game
    /// </summary>
    /// <returns></returns>
    public async Task<CreateGameResponse> CreateGame(CreateGameCommand data, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(ControllerNameGame, data, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<CreateGameResponse>(cancellationToken))!;
    }

    /// <summary>
    /// Returns game with participants - {gameId}
    /// </summary>
    public async Task<ListPlayersResponse> GetParticipants(Guid gameId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{ControllerNameGamePlayer}/{gameId}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ListPlayersResponse>(cancellationToken))!;
    }

    /// <summary>
    /// Join Game - join/{gameId}
    /// </summary>
    public async Task<JoinGameResponse> JoinGame(Guid gameId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerNameGame}/join/{gameId}", new {}, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<JoinGameResponse>(cancellationToken))!;
    }

    /// <summary>
    /// Leave a Game - leave/{gameId}
    /// </summary>
    public async Task<LeaveGameResponse> LeaveGame(Guid gameId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerNameGame}/leave/{gameId}", new { }, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<LeaveGameResponse>(cancellationToken))!;
    }
}