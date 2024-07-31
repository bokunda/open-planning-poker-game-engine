namespace OpenPlanningPoker.GameEngine.Client.Api;

public class GameEngineClient(IHttpClientFactory httpClientFactory) : IGameEngineClient
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(nameof(GameEngineClient));

    public IGameResource GameResource => new GameResource(_httpClient);
    public IGameSettingsResource GameSettingsResource => new GameSettingsResource(_httpClient);
    public ITicketResource TicketResource => new TicketResource(_httpClient);
    public IVoteResource VoteResource => new VoteResource(_httpClient);
}