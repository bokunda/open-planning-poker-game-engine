namespace OpenPlanningPoker.GameEngine.Api.Controllers.Votes;

[Route("api/[controller]")]
[ApiController]
public class VotesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public VotesController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns votes for a ticket
    /// </summary>
    /// <returns></returns>
    [HttpGet("{ticketId}")]
    public async Task<GetVotesResponseApi> Get(Guid ticketId, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetVotesQuery(ticketId), cancellationToken);
        return _mapper.Map<GetVotesResponseApi>(result);
    }

    /// <summary>
    /// Creates a vote
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<CreateVoteResponseApi> Post([FromBody] CreateVoteCommandApi command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(_mapper.Map<CreateVoteCommand>(command), cancellationToken);
        return _mapper.Map<CreateVoteResponseApi>(result);

    }

    /// <summary>
    /// Updates a vote
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<UpdateVoteResponseApi> Put([FromBody] UpdateVoteCommandApi command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(_mapper.Map<UpdateVoteCommand>(command), cancellationToken);
        return _mapper.Map<UpdateVoteResponseApi>(result);
    }
}