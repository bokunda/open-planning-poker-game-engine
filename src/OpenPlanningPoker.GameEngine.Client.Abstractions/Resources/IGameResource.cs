namespace OpenPlanningPoker.GameEngine.Client.Abstractions.Resources;

public interface IGameResource
{
    /// <summary>
    /// Returns game details - {id}
    /// </summary>
    /// <returns></returns>
    Task<GetGameResponse> GetGameDetails(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a game
    /// </summary>
    /// <returns></returns>
    Task<CreateGameResponse> CreateGame(CreateGameCommand data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns game with participants - {gameId}
    /// </summary>
    Task<ListPlayersResponse> GetParticipants(Guid gameId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Join Game - join/{gameId}
    /// </summary>
    Task<JoinGameResponse> JoinGame(Guid gameId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Leave a Game - leave/{gameId}
    /// </summary>
    Task<LeaveGameResponse> LeaveGame(Guid gameId, CancellationToken cancellationToken = default);
}