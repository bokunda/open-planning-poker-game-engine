namespace OpenPlanningPoker.GameEngine.Application.Features.Tickets;

public sealed record GetTicketsResponse(ICollection<GetTicketsItem> Tickets, int TotalCount);
public sealed record GetTicketsItem(Guid Id, Guid GameId, string Name, string Description);
public sealed record GetTicketsQuery(Guid GameId) : IRequest<GetTicketsResponse>;

public static class GetTickets
{
    public class Validator : AbstractValidator<GetTicketsQuery>
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
            CreateMap<Ticket, GetTicketsResponse>();
            CreateMap<Ticket, GetTicketsItem>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<GetTicketsQuery, GetTicketsResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public RequestHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<GetTicketsResponse> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            var data = await _ticketRepository.GetByGame(request.GameId, cancellationToken);
            var mappedData = _mapper.Map<ICollection<GetTicketsItem>>(data);
            return new GetTicketsResponse(mappedData, mappedData.Count);
        }
    }
}