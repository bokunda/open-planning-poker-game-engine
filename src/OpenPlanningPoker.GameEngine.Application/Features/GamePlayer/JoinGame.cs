namespace OpenPlanningPoker.GameEngine.Application.Features.GamePlayer;

public sealed record JoinGameResponse;

public sealed record JoinGameCommand(Guid GameId, Guid UserId) : IRequest<JoinGameResponse>;

public static class JoinGame
{
    public class Validator : AbstractValidator<JoinGameCommand>
    {
        public Validator()
        {
            RuleFor(x => x.GameId)
                .NotEmpty();

            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }

    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JoinGameCommand, Domain.GamePlayer.GamePlayer>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<JoinGameCommand, JoinGameResponse>
    {
        private readonly IGamePlayerRepository _gamePlayerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RequestHandler(IGamePlayerRepository gamePlayerRepository, IUnitOfWork unitOfWork)
        {
            _gamePlayerRepository = gamePlayerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<JoinGameResponse> Handle(JoinGameCommand request, CancellationToken cancellationToken = default)
        {
            var gamePlayer = Domain.GamePlayer.GamePlayer.Create(request.GameId, request.UserId);
            _gamePlayerRepository.Add(gamePlayer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new JoinGameResponse();
        }
    }
}