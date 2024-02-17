namespace OpenPlanningPoker.GameEngine.Application.Features.GameSettings;

public sealed record UpdateGameSettingsResponse(Guid Id, Guid GameId, int VotingTime, bool IsBreakAllowed);

public sealed record UpdateGameSettingsCommand(Guid Id, Guid GameId, int VotingTime, bool IsBreakAllowed) : IRequest<UpdateGameSettingsResponse>;

public static class UpdateGameSettings
{
    public class Validator : AbstractValidator<UpdateGameSettingsCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

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
            CreateMap<Domain.GameSettings.GameSettings, UpdateGameSettingsResponse>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<UpdateGameSettingsCommand, UpdateGameSettingsResponse>
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

        public async Task<UpdateGameSettingsResponse> Handle(UpdateGameSettingsCommand request, CancellationToken cancellationToken)
        {
            // Create a game
            var gameSettings = await _gameSettingsRepository.GetByIdAsync(request.Id, cancellationToken);
            gameSettings!.Update(request.GameId, request.VotingTime, request.IsBreakAllowed);
            _gameSettingsRepository.Update(gameSettings);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UpdateGameSettingsResponse>(gameSettings);
        }
    }
}