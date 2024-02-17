namespace OpenPlanningPoker.GameEngine.Api.Tests.Features.GamePlayer;

public class GamePlayerTests : BaseApiTests
{


    public GamePlayerTests(ApiTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ListPlayers_Valid()
    {
        // Arrange
        var gameId = new Guid("e3231ae3-1715-4865-ac00-4d73fef4cb3c");
        var game = (Game.Create("GAME_1", "DESC_1").WithId(gameId) as Game)!;

        var playerId = new Guid("e3231ae3-1996-4865-ac00-4d73fef4cb3c");
        var gamePlayer = Domain.GamePlayer.GamePlayer.Create(gameId, playerId);

        DbContext.Add(game);
        DbContext.Add(gamePlayer);
        await DbContext.SaveChangesAsync();

        var query = new ListPlayersQuery(gameId);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.GameId.Should().Be(gameId);
        result.Players.Count.Should().Be(1);
    }

    [Fact]
    public async Task JoinGame_Valid()
    {
        // Arrange
        var gameId = new Guid("e7777ae3-1715-1111-ac00-4d73fef4cb3c");
        var game = (Game.Create("GAME_1", "DESC_1").WithId(gameId) as Game)!;

        var playerId = new Guid("e9999ae3-1996-4444-ac00-4d73fef4cb3c");

        DbContext.Add(game);
        await DbContext.SaveChangesAsync();

        var query = new JoinGameCommand(gameId, playerId);

        // Act
        await Sender.Send(query);
        var count = await DbContext.Set<Domain.GamePlayer.GamePlayer>().QueryByPlayer(playerId).CountAsync();

        // Assert
        count.Should().Be(1);
    }

    [Fact]
    public async Task LeaveGame_Valid()
    {
        // Arrange
        var gameId = new Guid("e7777ae3-1715-4865-ac00-4d73fef4cb3c");
        var game = (Game.Create("GAME_1", "DESC_1").WithId(gameId) as Game)!;

        var playerId = new Guid("e9999ae3-1996-4865-ac00-4d73fef4cb3c");
        var gamePlayer = Domain.GamePlayer.GamePlayer.Create(gameId, playerId);

        DbContext.Add(game);
        DbContext.Add(gamePlayer);
        await DbContext.SaveChangesAsync();

        var query = new LeaveGameCommand(gameId, playerId);

        // Act
        await Sender.Send(query);
        var count = await DbContext.Set<Domain.GamePlayer.GamePlayer>().QueryByPlayer(playerId).CountAsync();

        // Assert
        count.Should().Be(0);
    }

}