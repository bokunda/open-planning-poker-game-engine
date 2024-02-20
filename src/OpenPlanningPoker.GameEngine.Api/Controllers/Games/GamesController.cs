namespace OpenPlanningPoker.GameEngine.Api.Controllers.Games;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GamesController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns game details
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<GetGameResponseApi> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetGameQuery(id), cancellationToken);
        return _mapper.Map<GetGameResponseApi>(result);
    }

    /// <summary>
    /// Creates a game
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<CreateGameResponseApi> Post([FromBody] CreateGameCommand createGameCommand, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(createGameCommand, cancellationToken);
        return _mapper.Map<CreateGameResponseApi>(result);
    }
}