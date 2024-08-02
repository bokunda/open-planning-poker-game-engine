namespace OpenPlanningPoker.GameEngine.Api.Controllers.GameSettings;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class GameSettingsController(ISender sender, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Returns game settings details
    /// </summary>
    /// <returns></returns>
    [HttpGet("{gameId}")]
    public async Task<GetGameSettingsResponseApi> Get(Guid gameId, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetGameSettingsQuery(gameId), cancellationToken);
        return mapper.Map<GetGameSettingsResponseApi>(result);
    }

    /// <summary>
    /// Creates a game settings
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<CreateGameSettingsResponseApi> Post([FromBody] CreateGameSettingsCommandApi command, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(mapper.Map<CreateGameSettingsCommand>(command), cancellationToken);
        return mapper.Map<CreateGameSettingsResponseApi>(result);
    }

    /// <summary>
    /// Updates a game settings
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<UpdateGameSettingsResponseApi> Put([FromBody] UpdateGameSettingsCommandApi command, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(mapper.Map<UpdateGameSettingsCommand>(command), cancellationToken);
        return mapper.Map<UpdateGameSettingsResponseApi>(result);
    }
}