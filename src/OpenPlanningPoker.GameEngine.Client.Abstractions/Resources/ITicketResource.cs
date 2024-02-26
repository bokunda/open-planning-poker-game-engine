namespace OpenPlanningPoker.GameEngine.Client.Abstractions.Resources;

public interface ITicketResource
{
    /// <summary>
    /// Returns Ticket details - {id}
    /// </summary>
    /// <returns></returns>
    Task<GetTicketResponse> GetTicket(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns Tickets for selected game - game/{gameId}
    /// </summary>
    /// <returns></returns>
    Task<ApiCollection<GetTicketsItem>> GetTickets(Guid gameId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a Ticket
    /// </summary>
    /// <returns></returns>
    Task<CreateTicketResponse> CreateTicket(CreateTicketCommand data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Import a list of Tickets using web API - import
    /// </summary>
    /// <returns></returns>
    Task<ApiCollection<ImportTicketItem>> ImportTicket(ImportTicketsCommand data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Import a list of Tickets using a CSV file - import/csv/{gameId}
    /// </summary>
    /// <returns></returns>
    Task<ApiCollection<ImportTicketItem>> ImportTicketsCsv(Guid gameId, IFormFile file, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing ticket
    /// </summary>
    /// <returns></returns>
    Task<UpdateTicketResponse> UpdateTicket(UpdateTicketCommand data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a Ticket - {id}
    /// </summary>
    /// <returns></returns>
    Task<DeleteTicketResponse> DeleteTicket(Guid id, CancellationToken cancellationToken = default);
}