namespace OpenPlanningPoker.GameEngine.Application.Features.Tickets;

public sealed record GetTicketsItem(Guid Id, Guid GameId, string Name, string Description);
public sealed record GetTicketsQuery(Guid GameId) : IRequest<ApiCollection<GetTicketsItem>>;

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
            CreateMap<Ticket, GetTicketsItem>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<GetTicketsQuery, ApiCollection<GetTicketsItem>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public RequestHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<ApiCollection<GetTicketsItem>> Handle(GetTicketsQuery request, CancellationToken cancellationToken = default)
        {
            var data = await _ticketRepository.GetByGame(request.GameId, cancellationToken);
            var mappedData = _mapper.Map<ICollection<GetTicketsItem>>(data);
            return new ApiCollection<GetTicketsItem>(mappedData, mappedData.Count);
        }
    }
}