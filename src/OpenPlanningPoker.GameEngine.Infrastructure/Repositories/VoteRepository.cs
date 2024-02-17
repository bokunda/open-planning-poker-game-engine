namespace OpenPlanningPoker.GameEngine.Infrastructure.Repositories;

public class VoteRepository : Repository<Vote, Guid>, IVoteRepository
{
    public VoteRepository(OpenPlanningPokerGameEngineDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ICollection<Vote>> GetByTicket(Guid ticketId, CancellationToken cancellationToken) =>
        await DbContext.Set<Vote>()
            .QueryByTicket(ticketId)
            .ToListAsync(cancellationToken);
}