namespace OpenPlanningPoker.GameEngine.Client.Api.Resources;

public class GameSettings
{
    private readonly HttpClient _httpClient;
    private const string ControllerName = nameof(GameSettings);

    public GameSettings(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Returns game settings details - {gameId}
    /// </summary>
    public async Task<GetGameSettingsResponse> GetGameSettings(Guid gameId, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"{ControllerName}/{gameId}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<GetGameSettingsResponse>(cancellationToken))!;
    }

    /// <summary>
    /// Creates a game settings
    /// </summary>
    public async Task<CreateGameSettingsResponse> CreateGameSettings(CreateGameSettingsCommand createGameCommand, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerName}", createGameCommand, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<CreateGameSettingsResponse>(cancellationToken))!;
    }

    /// <summary>
    /// Updates a game settings
    /// </summary>
    public async Task<UpdateGameSettingsResponse> UpdateGameSettings(UpdateGameSettingsCommand createGameCommand, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PutAsJsonAsync($"{ControllerName}", createGameCommand, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<UpdateGameSettingsResponse>(cancellationToken))!;
    }
}