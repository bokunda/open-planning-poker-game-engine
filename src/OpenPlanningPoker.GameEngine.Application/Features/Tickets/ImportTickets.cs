namespace OpenPlanningPoker.GameEngine.Application.Features.Tickets;

public sealed record ImportTicketsResponse(ICollection<ImportTicketItem> Tickets, int TotalCount);
public sealed record ImportTicketItem(string Name, string Description);

public sealed record ImportTicketsCommand(Guid GameId, ICollection<ImportTicketItem> Tickets) : IRequest<ImportTicketsResponse>;

public static class ImportTickets
{
    public class Validator : AbstractValidator<ImportTicketsCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Tickets)
                .NotEmpty();
        }
    }

    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ImportTicketsCommand, Ticket>();
            CreateMap<Ticket, ImportTicketItem>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<ImportTicketsCommand, ImportTicketsResponse>
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

        public async Task<ImportTicketsResponse> Handle(ImportTicketsCommand request, CancellationToken cancellationToken = default)
        {
            var tickets = request.Tickets.Select(ticket => Ticket.Create(request.GameId, ticket.Name, ticket.Description));
            _ticketRepository.AddRange(tickets);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var mappedTickets = _mapper.Map<ICollection<ImportTicketItem>>(tickets);
            return new ImportTicketsResponse(mappedTickets, mappedTickets.Count);
        }
    }
}