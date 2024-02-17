namespace OpenPlanningPoker.GameEngine.Api.Controllers.Votes;

[Route("api/[controller]")]
[ApiController]
public class VotesController : ControllerBase
{
    private readonly ISender _sender;

    public VotesController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Returns votes for a ticket
    /// </summary>
    /// <returns></returns>
    [HttpGet("{ticketId}")]
    public async Task<GetVotesResponse> Get(Guid ticketId)
    {
        return await _sender.Send(new GetVotesQuery(ticketId));
    }

    /// <summary>
    /// Creates a vote
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<CreateVoteResponse> Post([FromBody] CreateVoteCommand command)
    {
        return await _sender.Send(command);
    }

    /// <summary>
    /// Updates a vote
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<UpdateVoteResponse> Put([FromBody] UpdateVoteCommand command)
    {
        return await _sender.Send(command);
    }
}