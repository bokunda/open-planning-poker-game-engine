namespace OpenPlanningPoker.GameEngine.Application.Features.Votes;

public sealed record GetVotesResponse(Guid TicketId, ICollection<GetVotesVoteItem> Votes, int TotalCount);
public sealed record GetVotesVoteItem(Guid Id, Guid PlayerId, int Value);
public sealed record GetVotesQuery(Guid TicketId) : IRequest<GetVotesResponse>;

public static class GetVotes
{
    public class Validator : AbstractValidator<GetVotesQuery>
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
            CreateMap<Vote, GetVotesVoteItem>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<GetVotesQuery, GetVotesResponse>
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IMapper _mapper;

        public RequestHandler(IVoteRepository voteRepository, IMapper mapper)
        {
            _voteRepository = voteRepository;
            _mapper = mapper;
        }

        public async Task<GetVotesResponse> Handle(GetVotesQuery request, CancellationToken cancellationToken = default)
        {
            var result = await _voteRepository.GetByTicket(request.TicketId, cancellationToken);
            var mappedResult = _mapper.Map<ICollection<GetVotesVoteItem>>(result);
            return new GetVotesResponse(request.TicketId, mappedResult, mappedResult.Count);
        }
    }
}