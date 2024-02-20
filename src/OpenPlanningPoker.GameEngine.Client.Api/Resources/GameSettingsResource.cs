namespace OpenPlanningPoker.GameEngine.Client.Api.Resources;

public class GameSettingsResource : IGameSettingsResource
{
    private readonly HttpClient _httpClient;
    private const string ControllerName = nameof(GameSettingsResource);

    public GameSettingsResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetGameSettingsResponse> GetGameSettings(Guid gameId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{ControllerName}/{gameId}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<GetGameSettingsResponse>(cancellationToken))!;
    }

    public async Task<CreateGameSettingsResponse> CreateGameSettings(CreateGameSettingsCommand command, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerName}", command, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<CreateGameSettingsResponse>(cancellationToken))!;
    }

    public async Task<UpdateGameSettingsResponse> UpdateGameSettings(UpdateGameSettingsCommand command, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"{ControllerName}", command, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<UpdateGameSettingsResponse>(cancellationToken))!;
    }
}