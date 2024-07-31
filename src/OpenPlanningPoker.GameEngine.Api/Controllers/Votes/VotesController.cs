using GetVotesItemApi = OpenPlanningPoker.GameEngine.Api.Models.Features.Votes.GetVotesItem;

namespace OpenPlanningPoker.GameEngine.Api.Controllers.Votes;

[Route("api/[controller]")]
[ApiController]
public class VotesController(ISender sender, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Returns votes for a ticket
    /// </summary>
    /// <returns></returns>
    [HttpGet("{ticketId}")]
    public async Task<Models.ApiCollection<GetVotesItemApi>> Get(Guid ticketId, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetVotesQuery(ticketId), cancellationToken);
        return mapper.Map<Models.ApiCollection<GetVotesItemApi>>(result);
    }

    /// <summary>
    /// Creates a vote
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<CreateVoteResponseApi> Post([FromBody] CreateVoteCommandApi command, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(mapper.Map<CreateVoteCommand>(command), cancellationToken);
        return mapper.Map<CreateVoteResponseApi>(result);

    }

    /// <summary>
    /// Updates a vote
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<UpdateVoteResponseApi> Put([FromBody] UpdateVoteCommandApi command, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(mapper.Map<UpdateVoteCommand>(command), cancellationToken);
        return mapper.Map<UpdateVoteResponseApi>(result);
    }
}