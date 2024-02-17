namespace OpenPlanningPoker.GameEngine.Application.Features.GameSettings;

public sealed record GetGameSettingsResponse(Guid Id, Guid GameId, int VotingTime, bool IsBreakAllowed);
public sealed record GetGameSettingsQuery(Guid GameId) : IRequest<GetGameSettingsResponse>;

public static class GetGameSettings
{
    public class Validator : AbstractValidator<GetGameSettingsQuery>
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
            CreateMap<Domain.GameSettings.GameSettings, GetGameSettingsResponse>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<GetGameSettingsQuery, GetGameSettingsResponse>
    {
        private readonly IGameSettingsRepository _gameSettingsRepository;
        private readonly IMapper _mapper;

        public RequestHandler(IGameSettingsRepository gameSettingsRepository, IMapper mapper)
        {
            _gameSettingsRepository = gameSettingsRepository;
            _mapper = mapper;
        }

        public async Task<GetGameSettingsResponse> Handle(GetGameSettingsQuery request, CancellationToken cancellationToken)
        {
            var data = await _gameSettingsRepository.GetByGame(request.GameId, cancellationToken);
            return _mapper.Map<GetGameSettingsResponse>(data);
        }
    }
}