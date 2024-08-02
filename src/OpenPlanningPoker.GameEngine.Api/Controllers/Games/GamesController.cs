namespace OpenPlanningPoker.GameEngine.Api.Controllers.Games;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class GamesController(ISender sender, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Returns game details
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<GetGameResponseApi> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetGameQuery(id), cancellationToken);
        return mapper.Map<GetGameResponseApi>(result);
    }

    /// <summary>
    /// Creates a game
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<CreateGameResponseApi> Post([FromBody] CreateGameCommand createGameCommand, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(createGameCommand, cancellationToken);
        return mapper.Map<CreateGameResponseApi>(result);
    }
}