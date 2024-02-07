namespace OpenPlanningPoker.GameEngine.Api.Tests.Infrastructure;

public class ApiTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("open-planning-poker-game-engine-db")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<OpenPlanningPokerGameEngineDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<OpenPlanningPokerGameEngineDbContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString())
                    .UseSnakeCaseNamingConvention();
            });
        });
    }

    public Task InitializeAsync() => _dbContainer.StartAsync();

    public new Task DisposeAsync() => _dbContainer.StopAsync();
}