namespace OpenPlanningPoker.GameEngine.Infrastructure.Repositories;

public sealed class GameSettingsRepository : Repository<GameSettings, Guid>, IGameSettingsRepository
{
    public GameSettingsRepository(OpenPlanningPokerGameEngineDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<GameSettings> GetByGame(Guid gameId, CancellationToken cancellationToken = default) =>
        await DbContext.Set<GameSettings>()
            .QueryByGame(gameId)
            .FirstAsync(cancellationToken);

}