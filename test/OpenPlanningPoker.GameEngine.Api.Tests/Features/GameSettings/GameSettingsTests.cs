namespace OpenPlanningPoker.GameEngine.Api.Tests.Features.GameSettings;

public class GameSettingsTests : BaseApiTests
{
    public GameSettingsTests(ApiTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetGameSettings_Valid()
    {
        // Arrange
        var gameId = new Guid("e9999ae3-1715-4865-ac00-4d73fef4cb3c");
        var game = (Game.Create("GAME_GET", "DESC_1").WithId(gameId) as Game)!;

        const int votingTime = 60;
        const bool isBreakAllowed = true;

        var gameSettings = Domain.GameSettings.GameSettings.Create(gameId, votingTime, isBreakAllowed);
        await DbContext.AddAsync(game);
        await DbContext.AddAsync(gameSettings);

        await DbContext.SaveChangesAsync();

        var query = new GetGameSettingsQuery(gameId);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should().BeEquivalentTo(gameSettings, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task CreateGameSettings_Valid()
    {
        // Arrange
        var gameId = new Guid("e8888ae3-1715-4865-ac00-4d73fef4cb3c");
        var game = (Game.Create("GAME_CREATE", "DESC_1").WithId(gameId) as Game)!;

        await DbContext.AddAsync(game);
        await DbContext.SaveChangesAsync();

        const int votingTime = 160;
        const bool isBreakAllowed = true;
        
        var command = new CreateGameSettingsCommand(gameId, votingTime, isBreakAllowed);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.GameId.Should().Be(gameId);
        result.VotingTime.Should().Be(votingTime);
        result.IsBreakAllowed.Should().Be(isBreakAllowed);
    }

    [Fact]
    public async Task UpdateGameSettings_Valid()
    {
        // Arrange
        var gameId = new Guid("e7777ae3-1715-4865-ac00-4d73fef4cb3c");
        var game = (Game.Create("GAME_UPDATE", "DESC_1").WithId(gameId) as Game)!;

        const int votingTime = 60;
        const int votingTimeUpdated = 160;

        const bool isBreakAllowed = true;
        const bool isBreakAllowedUpdated = false;

        var gameSettings = Domain.GameSettings.GameSettings.Create(gameId, votingTime, isBreakAllowed);
        await DbContext.AddAsync(game);
        await DbContext.AddAsync(gameSettings);

        await DbContext.SaveChangesAsync();

        var command = new UpdateGameSettingsCommand(gameSettings.Id, gameId, votingTimeUpdated, isBreakAllowedUpdated);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Id.Should().Be(gameSettings.Id);
        result.GameId.Should().Be(gameId);
        result.VotingTime.Should().Be(votingTimeUpdated);
        result.IsBreakAllowed.Should().Be(isBreakAllowedUpdated);
    }
}