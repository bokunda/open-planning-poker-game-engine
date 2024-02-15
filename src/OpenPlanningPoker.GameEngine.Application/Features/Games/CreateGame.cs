using OpenPlanningPoker.GameEngine.Domain.Abstractions;

namespace OpenPlanningPoker.GameEngine.Application.Features.Games;

public sealed record CreateGameResponse(Guid Id, string Name, string Description);

public sealed record CreateGameCommand(string Name, string Description) : IRequest<CreateGameResponse>;

public static class CreateGame
{
    public class Validator : AbstractValidator<CreateGameCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(255)
                .NotEmpty();

            RuleFor(x => x.Description)
                .MaximumLength(4080);
        }
    }

    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateGameCommand, Game>();
            CreateMap<Game, CreateGameResponse>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<CreateGameCommand, CreateGameResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RequestHandler(IGameRepository gameRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateGameResponse> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var game = Game.Create(request.Name, request.Description);
            _gameRepository.Add(game);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CreateGameResponse>(game);
        }
    }
}