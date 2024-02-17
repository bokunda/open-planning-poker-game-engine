namespace OpenPlanningPoker.GameEngine.Api.Controllers.GameSettings;

[Route("api/[controller]")]
[ApiController]
public class GameSettingsController : ControllerBase
{
    private readonly ISender _sender;

    public GameSettingsController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Returns game settings details
    /// </summary>
    /// <returns></returns>
    [HttpGet("{gameId}")]
    public async Task<GetGameSettingsResponse> Get(Guid gameId, CancellationToken cancellationToken)
    {
        return await _sender.Send(new GetGameSettingsQuery(gameId), cancellationToken);
    }

    /// <summary>
    /// Creates a game settings
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<CreateGameSettingsResponse> Post([FromBody] CreateGameSettingsCommand createGameCommand, CancellationToken cancellationToken)
    {
        return await _sender.Send(createGameCommand, cancellationToken);
    }

    /// <summary>
    /// Updates a game settings
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<UpdateGameSettingsResponse> Put([FromBody] UpdateGameSettingsCommand createGameCommand, CancellationToken cancellationToken)
    {
        return await _sender.Send(createGameCommand, cancellationToken);
    }
}