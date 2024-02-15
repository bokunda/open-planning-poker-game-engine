using OpenPlanningPoker.GameEngine.Application.Features.Games;

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
    public async Task<GetGame.Response> Get(Guid id)
    {
        return await _sender.Send(new GetGame.Query(id));
    }
}