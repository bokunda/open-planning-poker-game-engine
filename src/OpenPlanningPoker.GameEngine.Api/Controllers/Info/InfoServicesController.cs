
namespace OpenPlanningPoker.GameEngine.Api.Controllers.Info;

[Route("api/[controller]")]
[ApiController]
public class InfoController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public InfoController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns info about the service
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<GetInfoResponseApi> Get(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetInfoQuery(), cancellationToken);
        return _mapper.Map<GetInfoResponseApi>(result);
    }
}