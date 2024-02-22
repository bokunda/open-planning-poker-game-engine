namespace OpenPlanningPoker.GameEngine.Client.Abstractions.Resources;

public interface IVoteResource
{
    /// <summary>
    /// Returns votes for a ticket - {ticketId}
    /// </summary>
    /// <returns></returns>
    Task<ApiCollection<GetVotesItem>> GetTicketVotes(Guid ticketId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a vote
    /// </summary>
    /// <returns></returns>
    Task<CreateVoteResponse> CreateVote(CreateVoteCommand data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a vote
    /// </summary>
    /// <returns></returns>
    Task<UpdateVoteResponse> UpdateVote(UpdateVoteCommand data, CancellationToken cancellationToken = default);
}