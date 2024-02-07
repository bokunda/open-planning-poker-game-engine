using OpenPlanningPoker.GameEngine.Application.Info.GetInfo;

namespace OpenPlanningPoker.GameEngine.Api.Controllers.Info;

[Route("api/[controller]")]
[ApiController]
public class InfoController : ControllerBase
{
    private readonly ISender _sender;

    public InfoController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Returns info about the service
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<GetInfoResponse> Get()
    {
        return await _sender.Send(new GetInfoQuery());
    }
}