namespace OpenPlanningPoker.GameEngine.Client.Abstractions.Resources;

public interface IGameSettingsResource
{
    /// <summary>
    /// Returns game settings details - {gameId}
    /// </summary>
    Task<GetGameSettingsResponse> GetGameSettings(Guid gameId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a game settings
    /// </summary>
    Task<CreateGameSettingsResponse> CreateGameSettings(CreateGameSettingsCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a game settings
    /// </summary>
    Task<UpdateGameSettingsResponse> UpdateGameSettings(UpdateGameSettingsCommand command, CancellationToken cancellationToken = default);
}