namespace OpenPlanningPoker.GameEngine.Application.Features.Tickets;

public sealed record DeleteTicketResponse;

public sealed record DeleteTicketCommand(Guid TicketId) : IRequest<DeleteTicketResponse>;

public static class DeleteTicket
{
    public class Validator : AbstractValidator<DeleteTicketCommand>
    {
        public Validator()
        {
            RuleFor(x => x.TicketId)
                .NotEmpty();
        }
    }

    public sealed class RequestHandler : IRequestHandler<DeleteTicketCommand, DeleteTicketResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RequestHandler(ITicketRepository ticketRepository, IUnitOfWork unitOfWork)
        {
            _ticketRepository = ticketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteTicketResponse> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = (await _ticketRepository.GetByIdAsync(request.TicketId, cancellationToken))!;
            _ticketRepository.Delete(ticket);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new DeleteTicketResponse();
        }
    }
}