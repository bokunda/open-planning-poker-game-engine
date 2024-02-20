namespace OpenPlanningPoker.GameEngine.Api.Models.Features.Votes;

public sealed record GetVotesResponse(Guid TicketId, ICollection<GetVotesVoteItem> Votes, int TotalCount);
public sealed record GetVotesVoteItem(Guid Id, Guid PlayerId, int Value);
public sealed record GetVotesQuery(Guid TicketId);
