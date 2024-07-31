namespace OpenPlanningPoker.GameEngine.Client.Api.Resources;

public class GameSettingsResource(HttpClient httpClient) : IGameSettingsResource
{
    private const string ControllerName = "gamesettings";

    public async Task<GetGameSettingsResponse> GetGameSettings(Guid gameId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"{ControllerName}/{gameId}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<GetGameSettingsResponse>(cancellationToken))!;
    }

    public async Task<CreateGameSettingsResponse> CreateGameSettings(CreateGameSettingsCommand command, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"{ControllerName}", command, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<CreateGameSettingsResponse>(cancellationToken))!;
    }

    public async Task<UpdateGameSettingsResponse> UpdateGameSettings(UpdateGameSettingsCommand command, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"{ControllerName}", command, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<UpdateGameSettingsResponse>(cancellationToken))!;
    }
}