namespace OpenPlanningPoker.GameEngine.Application.Features.Votes;

public sealed record GetVotesItem(Guid Id, Guid PlayerId, int Value);
public sealed record GetVotesQuery(Guid TicketId) : IRequest<ApiCollection<GetVotesItem>>;

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
            CreateMap<Vote, GetVotesItem>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<GetVotesQuery, ApiCollection<GetVotesItem>>
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IMapper _mapper;

        public RequestHandler(IVoteRepository voteRepository, IMapper mapper)
        {
            _voteRepository = voteRepository;
            _mapper = mapper;
        }

        public async Task<ApiCollection<GetVotesItem>> Handle(GetVotesQuery request, CancellationToken cancellationToken = default)
        {
            var result = await _voteRepository.GetByTicket(request.TicketId, cancellationToken);
            var mappedResult = _mapper.Map<ICollection<GetVotesItem>>(result);
            return new ApiCollection<GetVotesItem>(mappedResult, mappedResult.Count);
        }
    }
}