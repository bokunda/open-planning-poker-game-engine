namespace OpenPlanningPoker.GameEngine.Api.Controllers.GamePlayer;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class GamePlayerController(ISender sender, IMapper mapper, ICurrentUserProvider currentUserProvider)
    : ControllerBase
{
    /// <summary>
    /// Returns game with participants
    /// </summary>
    /// <returns></returns>
    [HttpGet("{gameId}")]
    public async Task<ApiCollection<ListPlayersPlayerItemApi>> Get(Guid gameId, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new ListPlayersQuery(gameId), cancellationToken);
        return mapper.Map<ApiCollection<ListPlayersPlayerItemApi>>(result);
    }

    /// <summary>
    /// Join Game
    /// </summary>
    /// <returns></returns>
    [HttpPost("join/{gameId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<JoinGameResponseApi> Join(Guid gameId, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new JoinGameCommand(gameId, currentUserProvider.CustomerId), cancellationToken);
        return mapper.Map<JoinGameResponseApi>(result);
    }

    /// <summary>
    /// Leave a Game
    /// </summary>
    /// <returns></returns>
    [HttpPost("leave/{gameId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<LeaveGameResponseApi> Leave(Guid gameId, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new LeaveGameCommand(gameId, currentUserProvider.CustomerId), cancellationToken);
        return mapper.Map<LeaveGameResponseApi>(result);
    }
}