namespace OpenPlanningPoker.GameEngine.Api.Controllers.Games;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly ISender _sender;

    public GamesController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Returns game details
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<GetGameResponse> Get(Guid id)
    {
        return await _sender.Send(new GetGameQuery(id));
    }

    /// <summary>
    /// Creates a game
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<CreateGameResponse> Post([FromBody] CreateGameCommand createGameCommand)
    {
        return await _sender.Send(createGameCommand);
    }
}