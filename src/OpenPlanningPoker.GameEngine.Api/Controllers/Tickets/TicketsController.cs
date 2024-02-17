using CsvHelper;

namespace OpenPlanningPoker.GameEngine.Api.Controllers.Tickets;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ISender _sender;

    public TicketsController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Returns Ticket details
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<GetTicketResponse> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _sender.Send(new GetTicketQuery(id), cancellationToken);
    }

    /// <summary>
    /// Returns Tickets for selected game
    /// </summary>
    /// <returns></returns>
    [HttpGet("game/{gameId}")]
    public async Task<GetTicketsResponse> GetTickets(Guid gameId, CancellationToken cancellationToken)
    {
        return await _sender.Send(new GetTicketsQuery(gameId), cancellationToken);
    }

    /// <summary>
    /// Creates a Ticket
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<CreateTicketResponse> Post([FromBody] CreateTicketCommand createTicketCommand, CancellationToken cancellationToken)
    {
        return await _sender.Send(createTicketCommand, cancellationToken);
    }

    /// <summary>
    /// Import a list of Tickets using web API
    /// </summary>
    /// <returns></returns>
    [HttpPost("import")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ImportTicketsResponse> Import([FromBody] ImportTicketsCommand command, CancellationToken cancellationToken)
    {
        return await _sender.Send(command, cancellationToken);
    }

    /// <summary>
    /// Import a list of Tickets using a CSV file
    /// </summary>
    /// <returns></returns>
    [HttpPost("import/csv/{gameId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ImportTicketsResponse> ImportCsv(Guid gameId, IFormFile file, CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var command = new ImportTicketsCommand(gameId, csv.GetRecords<ImportTicketItem>().ToList());

        return await _sender.Send(command, cancellationToken);
    }

    /// <summary>
    /// Delete a Ticket
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<DeleteTicketResponse> Delete(Guid id, CancellationToken cancellationToken)
    {
        return await _sender.Send(new DeleteTicketCommand(id), cancellationToken);
    }
}