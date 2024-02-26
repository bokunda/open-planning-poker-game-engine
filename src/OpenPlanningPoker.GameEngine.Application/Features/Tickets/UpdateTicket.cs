namespace OpenPlanningPoker.GameEngine.Application.Features.Tickets;

public sealed record UpdateTicketResponse(Guid Id, Guid GameId, string Name, string Description);

public sealed record UpdateTicketCommand(Guid TicketId, string Name, string Description) : IRequest<UpdateTicketResponse>;

public static class UpdateTicket
{
    public class Validator : AbstractValidator<UpdateTicketCommand>
    {
        public Validator()
        {
            RuleFor(x => x.TicketId)
                .NotEmpty();

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
            CreateMap<UpdateTicketCommand, Ticket>();
            CreateMap<Ticket, UpdateTicketResponse>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<UpdateTicketCommand, UpdateTicketResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RequestHandler(ITicketRepository ticketRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateTicketResponse> Handle(UpdateTicketCommand request, CancellationToken cancellationToken = default)
        {
            var ticket = await _ticketRepository.GetByIdAsync(request.TicketId, cancellationToken);
            ticket!.Update(request.Name, request.Description);

            _ticketRepository.Update(ticket);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UpdateTicketResponse>(ticket);
        }
    }
}