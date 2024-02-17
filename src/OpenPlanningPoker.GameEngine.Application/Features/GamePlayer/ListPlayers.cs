namespace OpenPlanningPoker.GameEngine.Application.Features.GamePlayer;

public sealed record ListPlayersResponse(Guid GameId, ICollection<ListPlayersPlayerItem> Players, int TotalCount);
public sealed record ListPlayersPlayerItem(Guid Id, string Name);

public sealed record ListPlayersQuery(Guid GameId) : IRequest<ListPlayersResponse>;

public static class ListPlayers
{
    public class Validator : AbstractValidator<ListPlayersQuery>
    {
        public Validator()
        {
            RuleFor(x => x.GameId)
                .NotEmpty();
        }
    }

    public sealed class RequestHandler : IRequestHandler<ListPlayersQuery, ListPlayersResponse>
    {
        private readonly IGamePlayerRepository _gamePlayerRepository;

        public RequestHandler(IGamePlayerRepository gamePlayerRepository)
        {
            _gamePlayerRepository = gamePlayerRepository;
        }

        public async Task<ListPlayersResponse> Handle(ListPlayersQuery request, CancellationToken cancellationToken)
        {
            var gamePlayers = await _gamePlayerRepository.GetByGame(request.GameId, cancellationToken);

            return new ListPlayersResponse(request.GameId,
                gamePlayers.Select(x => new ListPlayersPlayerItem(x.PlayerId, "TODO")).ToList(), 
                gamePlayers.Count);
        }
    }
}