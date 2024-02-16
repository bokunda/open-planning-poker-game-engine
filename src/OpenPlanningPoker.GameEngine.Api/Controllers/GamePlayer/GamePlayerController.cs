using OpenPlanningPoker.GameEngine.Application.Features.GamePlayer;
using OpenPlanningPoker.GameEngine.Application.Features.Games;
using OpenPlanningPoker.GameEngine.Domain.Identity;

namespace OpenPlanningPoker.GameEngine.Api.Controllers.GamePlayer;

[Route("api/[controller]")]
[ApiController]
public class GamePlayerController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ICurrentUserProvider _currentUserProvider;

    public GamePlayerController(ISender sender, ICurrentUserProvider currentUserProvider)
    {
        _sender = sender;
        _currentUserProvider = currentUserProvider;
    }

    /// <summary>
    /// Returns game with participants
    /// </summary>
    /// <returns></returns>
    [HttpGet("{gameId}")]
    public async Task<ListPlayersResponse> Get(Guid gameId)
    {
        return await _sender.Send(new ListPlayersQuery(gameId));
    }

    /// <summary>
    /// Join Game
    /// </summary>
    /// <returns></returns>
    [HttpPost("join/{gameId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<JoinGameResponse> Join(Guid gameId)
    {
        return await _sender.Send(new JoinGameCommand(gameId, _currentUserProvider.CustomerId));
    }

    /// <summary>
    /// Leave a Game
    /// </summary>
    /// <returns></returns>
    [HttpPost("leave/{gameId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<LeaveGameResponse> Leave(Guid gameId)
    {
        return await _sender.Send(new LeaveGameCommand(gameId, _currentUserProvider.CustomerId));
    }
}