using CsvHelper;

namespace OpenPlanningPoker.GameEngine.Api.Controllers.Tickets;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public TicketsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns Ticket details
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<GetTicketResponseApi> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetTicketQuery(id), cancellationToken);
        return _mapper.Map<GetTicketResponseApi>(result);
    }

    /// <summary>
    /// Returns Tickets for selected game
    /// </summary>
    /// <returns></returns>
    [HttpGet("game/{gameId}")]
    public async Task<GetTicketsResponseApi> GetTickets(Guid gameId, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetTicketsQuery(gameId), cancellationToken);
        return _mapper.Map<GetTicketsResponseApi>(result);
    }

    /// <summary>
    /// Creates a Ticket
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<CreateTicketResponseApi> Post([FromBody] CreateTicketCommandApi command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(_mapper.Map<CreateTicketCommand>(command), cancellationToken);
        return _mapper.Map<CreateTicketResponseApi>(result);
    }

    /// <summary>
    /// Import a list of Tickets using web API
    /// </summary>
    /// <returns></returns>
    [HttpPost("import")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ImportTicketsResponseApi> Import([FromBody] ImportTicketsCommandApi command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(_mapper.Map<ImportTicketsCommand>(command), cancellationToken);
        return _mapper.Map<ImportTicketsResponseApi>(result);
    }

    /// <summary>
    /// Import a list of Tickets using a CSV file
    /// </summary>
    /// <returns></returns>
    [HttpPost("import/csv/{gameId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ImportTicketsResponseApi> ImportCsv(Guid gameId, IFormFile file, CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var command = new ImportTicketsCommand(gameId, csv.GetRecords<ImportTicketItem>().ToList());

        var result = await _sender.Send(command, cancellationToken);
        return _mapper.Map<ImportTicketsResponseApi>(result);
    }

    /// <summary>
    /// Delete a Ticket
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<DeleteTicketResponseApi> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteTicketCommand(id), cancellationToken);
        return _mapper.Map<DeleteTicketResponseApi>(result);
    }
}