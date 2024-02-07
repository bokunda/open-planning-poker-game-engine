namespace OpenPlanningPoker.GameEngine.Api.Tests;

public abstract class BaseApiTests : IClassFixture<ApiTestWebAppFactory>
{
    protected readonly ISender Sender;
    protected readonly OpenPlanningPokerGameEngineDbContext DbContext;

    protected BaseApiTests(ApiTestWebAppFactory factory)
    {
        var scope = factory.Services.CreateScope();

        Sender = scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = scope.ServiceProvider.GetRequiredService<OpenPlanningPokerGameEngineDbContext>();
    }
}