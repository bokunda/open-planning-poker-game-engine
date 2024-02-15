namespace OpenPlanningPoker.GameEngine.Application.Features.Games;

public static class GetGame
{
    public sealed record Response(Guid Id, string Name, string Description);

    public sealed record Query(Guid GameId) : IRequest<Response>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.GameId).NotEmpty();
        }
    }

    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, Response>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<Query, Response>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public RequestHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var data = await _gameRepository.GetByIdAsync(request.GameId, cancellationToken);
            return _mapper.Map<Response>(data);
        }
    }
}