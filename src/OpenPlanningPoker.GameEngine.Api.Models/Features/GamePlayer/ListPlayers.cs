namespace OpenPlanningPoker.GameEngine.Api.Models.Features.GamePlayer;

public sealed record ListPlayersResponse(Guid GameId, ICollection<ListPlayersPlayerItem> Players, int TotalCount);
public sealed record ListPlayersPlayerItem(Guid Id, string Name);
public sealed record ListPlayersQuery(Guid GameId);
    