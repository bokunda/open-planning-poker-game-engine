namespace OpenPlanningPoker.GameEngine.Client.Api.Extensions;

public static class DependencyInjection
{
    /// <summary>
    /// Adds GameEngine client through Dependency injection
    /// Example: builder.Services.AddGameEngineClient("https://host.docker.internal:6992/api/");
    /// </summary>
    /// <param name="services"></param>
    /// <param name="baseAddress"></param>
    public static IServiceCollection AddGameEngineClient(this IServiceCollection services, string baseAddress)
    {
        services.AddHttpClient<GameEngineClient>(nameof(GameEngineClient), client =>
        {
            client.BaseAddress = new Uri(baseAddress);
        })
        #if DEBUG
        .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        });
        #endif

        services.AddTransient<IGameEngineClient, GameEngineClient>();
        return services;
    }
}

