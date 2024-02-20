namespace OpenPlanningPoker.GameEngine.Api.Controllers.GamePlayer;

[Route("api/[controller]")]
[ApiController]
public class GamePlayerController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly ICurrentUserProvider _currentUserProvider;

    public GamePlayerController(ISender sender, IMapper mapper, ICurrentUserProvider currentUserProvider)
    {
        _sender = sender;
        _mapper = mapper;
        _currentUserProvider = currentUserProvider;
    }

    /// <summary>
    /// Returns game with participants
    /// </summary>
    /// <returns></returns>
    [HttpGet("{gameId}")]
    public async Task<ListPlayersResponseApi> Get(Guid gameId, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new ListPlayersQuery(gameId), cancellationToken);
        return _mapper.Map<ListPlayersResponseApi>(result);
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
        var result = await _sender.Send(new JoinGameCommand(gameId, _currentUserProvider.CustomerId), cancellationToken);
        return _mapper.Map<JoinGameResponseApi>(result);
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
        var result = await _sender.Send(new LeaveGameCommand(gameId, _currentUserProvider.CustomerId), cancellationToken);
        return _mapper.Map<LeaveGameResponseApi>(result);
    }
}