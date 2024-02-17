namespace OpenPlanningPoker.GameEngine.Application.Features.Votes;

public sealed record UpdateVoteResponse(Guid Id, Guid PlayerId, Guid TicketId, int Value);

public sealed record UpdateVoteCommand(Guid Id, int Value) : IRequest<UpdateVoteResponse>;

public static class UpdateVote
{
    public class Validator : AbstractValidator<UpdateVoteCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
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
            CreateMap<UpdateVoteCommand, Vote>();
            CreateMap<Vote, UpdateVoteResponse>();
        }
    }

    public sealed class RequestHandler : IRequestHandler<UpdateVoteCommand, UpdateVoteResponse>
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RequestHandler(IVoteRepository voteRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _voteRepository = voteRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateVoteResponse> Handle(UpdateVoteCommand request, CancellationToken cancellationToken)
        {
            var vote = await _voteRepository.GetByIdAsync(request.Id, cancellationToken);
            vote!.Update(request.Value);
            _voteRepository.Update(vote);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UpdateVoteResponse>(vote);
        }
    }
}