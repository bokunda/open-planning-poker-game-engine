namespace OpenPlanningPoker.GameEngine.Application.Features.Games;

public sealed record GetGameResponse(Guid Id, string Name, string Description);
public sealed record GetGameQuery(Guid GameId) : IRequest<GetGameResponse>;

public static class GetGame
{
    public class Validator : AbstractValidator<GetGameQuery>
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
            CreateMap<Game, GetGameResponse>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<GetGameQuery, GetGameResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public RequestHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<GetGameResponse> Handle(GetGameQuery request, CancellationToken cancellationToken)
        {
            var data = await _gameRepository.GetByIdAsync(request.GameId, cancellationToken);
            return _mapper.Map<GetGameResponse>(data);
        }
    }
}