namespace OpenPlanningPoker.GameEngine.Client.Api;

public class GameEngineClient
{
    private HttpClient _httpClient;

    public GameEngineClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(nameof(GameEngineClient));
    }

    public Games Games => new(_httpClient);
    public GameSettings GameSettings => new(_httpClient);
    public Tickets Tickets => new(_httpClient);
    public Votes Votes => new(_httpClient);
}