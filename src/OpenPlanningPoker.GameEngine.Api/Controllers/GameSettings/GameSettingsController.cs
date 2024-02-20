namespace OpenPlanningPoker.GameEngine.Api.Controllers.GameSettings;

[Route("api/[controller]")]
[ApiController]
public class GameSettingsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GameSettingsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns game settings details
    /// </summary>
    /// <returns></returns>
    [HttpGet("{gameId}")]
    public async Task<GetGameSettingsResponseApi> Get(Guid gameId, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetGameSettingsQuery(gameId), cancellationToken);
        return _mapper.Map<GetGameSettingsResponseApi>(result);
    }

    /// <summary>
    /// Creates a game settings
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<CreateGameSettingsResponseApi> Post([FromBody] CreateGameSettingsCommandApi command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(_mapper.Map<CreateGameSettingsCommand>(command), cancellationToken);
        return _mapper.Map<CreateGameSettingsResponseApi>(result);
    }

    /// <summary>
    /// Updates a game settings
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<UpdateGameSettingsResponseApi> Put([FromBody] UpdateGameSettingsCommandApi command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(_mapper.Map<UpdateGameSettingsCommand>(command), cancellationToken);
        return _mapper.Map<UpdateGameSettingsResponseApi>(result);
    }
}