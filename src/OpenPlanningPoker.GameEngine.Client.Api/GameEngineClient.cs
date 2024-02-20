﻿namespace OpenPlanningPoker.GameEngine.Client.Api;

public class GameEngineClient : IGameEngineClient
{
    private readonly HttpClient _httpClient;

    public GameEngineClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(nameof(GameEngineClient));
    }

    public IGameResource GameResource => new GameResource(_httpClient);
    public IGameSettingsResource GameSettingsResource => new GameSettingsResource(_httpClient);
    public ITicketResource TicketResource => new TicketResource(_httpClient);
    public IVoteResource VoteResource => new VoteResource(_httpClient);
}