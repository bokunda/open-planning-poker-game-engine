namespace OpenPlanningPoker.GameEngine.Infrastructure.Repositories;

public sealed class GameRepository : Repository<Game, Guid>, IGameRepository
{
    public GameRepository(OpenPlanningPokerGameEngineDbContext dbContext) : base(dbContext)
    {
    }
}