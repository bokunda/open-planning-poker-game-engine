
namespace OpenPlanningPoker.GameEngine.Api.Controllers.Info;

[Route("api/[controller]")]
[ApiController]
public class InfoController(ISender sender, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Returns info about the service
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<GetInfoResponseApi> Get(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetInfoQuery(), cancellationToken);
        return mapper.Map<GetInfoResponseApi>(result);
    }
}