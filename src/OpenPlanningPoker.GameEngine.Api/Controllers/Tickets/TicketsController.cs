using CsvHelper;

namespace OpenPlanningPoker.GameEngine.Api.Controllers.Tickets;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TicketsController(ISender sender, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Returns Ticket details
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<GetTicketResponseApi> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetTicketQuery(id), cancellationToken);
        return mapper.Map<GetTicketResponseApi>(result);
    }

    /// <summary>
    /// Returns Tickets for selected game
    /// </summary>
    /// <returns></returns>
    [HttpGet("game/{gameId}")]
    public async Task<Models.ApiCollection<GetTicketResponseApi>> GetTickets(Guid gameId, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetTicketsQuery(gameId), cancellationToken);
        return mapper.Map<Models.ApiCollection<GetTicketResponseApi>>(result);
    }

    /// <summary>
    /// Creates a Ticket
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<CreateTicketResponseApi> Post([FromBody] CreateTicketCommandApi command, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(mapper.Map<CreateTicketCommand>(command), cancellationToken);
        return mapper.Map<CreateTicketResponseApi>(result);
    }

    /// <summary>
    /// Import a list of Tickets using web API
    /// </summary>
    /// <returns></returns>
    [HttpPost("import")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<Models.ApiCollection<ImportTicketItemResponse>> Import([FromBody] ImportTicketsCommandApi command, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(mapper.Map<ImportTicketsCommand>(command), cancellationToken);
        return mapper.Map<Models.ApiCollection<ImportTicketItemResponse>>(result);
    }

    /// <summary>
    /// Import a list of Tickets using a CSV file
    /// </summary>
    /// <returns></returns>
    [HttpPost("import/csv/{gameId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<Models.ApiCollection<ImportTicketItemResponse>> ImportCsv(Guid gameId, IFormFile file, CancellationToken cancellationToken = default)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var command = new ImportTicketsCommand(gameId, csv.GetRecords<ImportTicketItem>().ToList());

        var result = await sender.Send(command, cancellationToken);
        return mapper.Map<Models.ApiCollection<ImportTicketItemResponse>>(result);
    }

    /// <summary>
    /// Update an existing ticket
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<UpdateTicketResponse> Update([FromBody] UpdateTicketCommand command, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(command, cancellationToken);
        return mapper.Map<UpdateTicketResponse>(result);
    }

    /// <summary>
    /// Delete a Ticket
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<DeleteTicketResponseApi> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new DeleteTicketCommand(id), cancellationToken);
        return mapper.Map<DeleteTicketResponseApi>(result);
    }
}