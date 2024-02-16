using FluentAssertions;
using OpenPlanningPoker.GameEngine.Api.Tests.Extensions;
using OpenPlanningPoker.GameEngine.Application.Features.Games;
using OpenPlanningPoker.GameEngine.Domain.Games;

namespace OpenPlanningPoker.GameEngine.Api.Tests.Features.Games;

public class GamesTests : BaseApiTests
{
    private readonly Game _firstGame = (Game.Create("GAME_1", "DESC_1").WithId(_firstGameId) as Game)!;
    private static readonly Guid _firstGameId = new ("e6652ae3-1715-4865-ac00-4d73fef4cb3c");

    public GamesTests(ApiTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetGame_Valid()
    {
        // Arrange
        DbContext.Add(_firstGame);
        await DbContext.SaveChangesAsync();

        var query = new GetGameQuery(_firstGameId);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should()
            .BeEquivalentTo(_firstGame, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task GetGame_GameDoesNotExists()
    {
        // Arrange
        var query = new GetGameQuery(Guid.Empty);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateGame_Success()
    {
        // Arrange
        var command = new CreateGameCommand("CREATE_GAME", "TEST");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Should().BeEquivalentTo(command, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task DeleteGame_Success()
    {
        // Arrange
        var game = Game.Create("DELETE_GAME", "TEST");
        DbContext.Add(game);
        await DbContext.SaveChangesAsync();

        var command = new DeleteGameCommand(game.Id);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Should().NotBeNull();
    }
}