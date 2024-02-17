namespace OpenPlanningPoker.GameEngine.Application.Features.Votes;

public sealed record CreateVoteResponse(Guid Id, Guid PlayerId, Guid TicketId, int Value);

public sealed record CreateVoteCommand(Guid TicketId, int Value) : IRequest<CreateVoteResponse>;

public static class CreateVote
{
    public class Validator : AbstractValidator<CreateVoteCommand>
    {
        public Validator()
        {
            RuleFor(x => x.TicketId)
                .NotEmpty();

            RuleFor(x => x.Value)
                .GreaterThan(0)
                .LessThan(100);
        }
    }

    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateVoteCommand, Vote>();
            CreateMap<Vote, CreateVoteResponse>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<CreateVoteCommand, CreateVoteResponse>
    {
        private readonly IVoteRepository _voteRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RequestHandler(IVoteRepository voteRepository,
            ICurrentUserProvider currentUserProvider, 
            IMapper mapper, 
            IUnitOfWork unitOfWork)
        {
            _voteRepository = voteRepository;
            _currentUserProvider = currentUserProvider;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateVoteResponse> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
        {
            var vote = Vote.Create(_currentUserProvider.CustomerId, request.TicketId, request.Value);
            _voteRepository.Add(vote);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CreateVoteResponse>(vote);
        }
    }
}