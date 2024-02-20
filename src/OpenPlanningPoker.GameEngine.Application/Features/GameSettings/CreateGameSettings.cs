namespace OpenPlanningPoker.GameEngine.Application.Features.GameSettings;

public sealed record CreateGameSettingsResponse(Guid Id, Guid GameId, int VotingTime, bool IsBreakAllowed);

public sealed record CreateGameSettingsCommand(Guid GameId, int VotingTime, bool IsBreakAllowed) : IRequest<CreateGameSettingsResponse>;

public static class CreateGameSettings
{
    public class Validator : AbstractValidator<CreateGameSettingsCommand>
    {
        public Validator()
        {
            RuleFor(x => x.GameId)
                .NotEmpty();

            RuleFor(x => x.VotingTime)
                .GreaterThan(0);

            RuleFor(x => x.IsBreakAllowed)
                .NotEmpty();
        }
    }

    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateGameSettingsCommand, Domain.GameSettings.GameSettings>();
            CreateMap<Domain.GameSettings.GameSettings, CreateGameSettingsResponse>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<CreateGameSettingsCommand, CreateGameSettingsResponse>
    {
        private readonly IGameSettingsRepository _gameSettingsRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RequestHandler(IGameSettingsRepository gameSettingsRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _gameSettingsRepository = gameSettingsRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateGameSettingsResponse> Handle(CreateGameSettingsCommand request, CancellationToken cancellationToken = default)
        {
            var game = Domain.GameSettings.GameSettings.Create(request.GameId, request.VotingTime, request.IsBreakAllowed);
            _gameSettingsRepository.Add(game);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CreateGameSettingsResponse>(game);
        }
    }
}