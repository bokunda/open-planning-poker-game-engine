namespace OpenPlanningPoker.GameEngine.Application.Features.GamePlayer;

public sealed record ListPlayersResponse(ListPlayersGameItem Game, ICollection<ListPlayersPlayerItem> Players, int TotalCount);
public sealed record ListPlayersGameItem(Guid Id, string Name, string Description);
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

    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, ListPlayersGameItem>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<ListPlayersQuery, ListPlayersResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGamePlayerRepository _gamePlayerRepository;
        private readonly IMapper _mapper;

        public RequestHandler(
            IGameRepository gameRepository,
            IGamePlayerRepository gamePlayerRepository,
            IMapper mapper)
        {
            _gameRepository = gameRepository;
            _gamePlayerRepository = gamePlayerRepository;
            _mapper = mapper;
        }

        public async Task<ListPlayersResponse> Handle(ListPlayersQuery request, CancellationToken cancellationToken)
        {
            var game = await _gameRepository.GetByIdAsync(request.GameId, cancellationToken);
            var gamePlayers = await _gamePlayerRepository.GetByGame(request.GameId, cancellationToken);

            return new ListPlayersResponse(
                _mapper.Map<ListPlayersGameItem>(game), 
                gamePlayers.Select(x => new ListPlayersPlayerItem(x.PlayerId, "TODO")).ToList(), 
                gamePlayers.Count);
        }
    }
}