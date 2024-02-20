namespace OpenPlanningPoker.GameEngine.Api.Models.Features.Tickets;

public sealed record GetTicketsResponse(ICollection<GetTicketsItem> Tickets, int TotalCount);
public sealed record GetTicketsItem(Guid Id, Guid GameId, string Name, string Description);
public sealed record GetTicketsQuery(Guid GameId);
