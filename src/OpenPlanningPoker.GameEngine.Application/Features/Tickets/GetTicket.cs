namespace OpenPlanningPoker.GameEngine.Application.Features.Tickets;

public sealed record GetTicketResponse(Guid Id, Guid GameId, string Name, string Description);
public sealed record GetTicketQuery(Guid TicketId) : IRequest<GetTicketResponse>;

public static class GetTicket
{
    public class Validator : AbstractValidator<GetTicketQuery>
    {
        public Validator()
        {
            RuleFor(x => x.TicketId).NotEmpty();
        }
    }

    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ticket, GetTicketResponse>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<GetTicketQuery, GetTicketResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public RequestHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<GetTicketResponse> Handle(GetTicketQuery request, CancellationToken cancellationToken = default)
        {
            var data = await _ticketRepository.GetByIdAsync(request.TicketId, cancellationToken);
            return _mapper.Map<GetTicketResponse>(data);
        }
    }
}