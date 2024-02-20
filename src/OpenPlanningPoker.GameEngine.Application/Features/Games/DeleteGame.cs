namespace OpenPlanningPoker.GameEngine.Application.Features.Games;

public sealed record DeleteGameResponse;

public sealed record DeleteGameCommand(Guid GameId) : IRequest<DeleteGameResponse>;

/// <summary>
/// Delete Game will be used by the service worker for DB cleanup purposes so no validation is needed.
/// </summary>
public static class DeleteGame
{
    public class Validator : AbstractValidator<DeleteGameCommand>
    {
        public Validator()
        {
            RuleFor(x => x.GameId)
                .NotEmpty();
        }
    }

    public sealed class RequestHandler : IRequestHandler<DeleteGameCommand, DeleteGameResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RequestHandler(IGameRepository gameRepository, IUnitOfWork unitOfWork)
        {
            _gameRepository = gameRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteGameResponse> Handle(DeleteGameCommand request, CancellationToken cancellationToken = default)
        {
            var game = (await _gameRepository.GetByIdAsync(request.GameId, cancellationToken))!;
            _gameRepository.Delete(game);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new DeleteGameResponse();
        }
    }
}