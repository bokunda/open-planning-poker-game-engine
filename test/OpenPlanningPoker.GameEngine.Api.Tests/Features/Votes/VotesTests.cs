using OpenPlanningPoker.GameEngine.Application.Features.Votes;
using OpenPlanningPoker.GameEngine.Domain.Votes;

namespace OpenPlanningPoker.GameEngine.Api.Tests.Features.Votes;

public class VotesTests : BaseApiTests
{


    public VotesTests(ApiTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetVotes_Valid()
    {
        // Arrange

        var gameId = new Guid("e2323ae3-1715-4865-ac00-4d73fef4cb3c");
        var game = (Game.Create("GAME_1", "DESC_1").WithId(gameId) as Game)!;

        var ticketId = new Guid("e2323ae3-3232-4865-ac00-4d73fef4cb3c");
        var ticket = (Ticket.Create(gameId, "TICKET_1", "Desc").WithId(ticketId) as Ticket)!;

        var firstVoteId = new Guid("e2323ae3-1111-4865-ac00-4d73fef4cb3c");
        var secondVoteId = new Guid("e2323ae3-2222-4865-ac00-4d73fef4cb3c");

        var firstPlayerId = new Guid("e2323ae3-1111-4865-ac11-4d73fef4cb3c");
        var secondPlayerId = new Guid("e2323ae3-2222-4865-ac11-4d73fef4cb3c");

        const int firstVoteValue = 10;
        const int secondVoteValue = 20;

        var firstVote = (Vote.Create(firstPlayerId, ticketId, firstVoteValue).WithId(firstVoteId) as Vote)!;
        var secondVote = (Vote.Create(secondPlayerId, ticketId, secondVoteValue).WithId(secondVoteId) as Vote)!;
        var votes = new List<Vote> { firstVote, secondVote };

        DbContext.AddRange(game, ticket, firstVote, secondVote);

        await DbContext.SaveChangesAsync();

        var query = new GetVotesQuery(ticketId);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.TicketId.Should().Be(ticketId);
        result.Votes.Should().BeEquivalentTo(votes, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task CreateVote_GameDoesNotExists()
    {
        // Arrange
        var gameId = new Guid("a1234ae3-1715-4865-ac00-4d73fef4cb3c");
        var game = (Game.Create("GAME_2", "DESC_2").WithId(gameId) as Game)!;

        var ticketId = new Guid("b1234ae3-3232-4865-ac00-4d73fef4cb3c");
        var ticket = (Ticket.Create(gameId, "TICKET_2", "Desc").WithId(ticketId) as Ticket)!;

        DbContext.AddRange(game, ticket);
        await DbContext.SaveChangesAsync();

        const int voteValue = 10;

        var command = new CreateVoteCommand(ticketId, voteValue);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.TicketId.Should().Be(ticketId);
        result.Value.Should().Be(voteValue);
    }

    [Fact]
    public async Task UpdateVote_Success()
    {
        // Arrange
        var gameId = new Guid("c3333ae3-1715-4865-ac00-4d73fef4cb3c");
        var game = (Game.Create("GAME_3", "DESC_3").WithId(gameId) as Game)!;

        var ticketId = new Guid("d3333ae3-3232-4865-ac00-4d73fef4cb3c");
        var ticket = (Ticket.Create(gameId, "TICKET_3", "Desc").WithId(ticketId) as Ticket)!;

        var playerId = new Guid("e2323ae3-1111-4865-ac11-4d73fef4cb3c");

        var voteId = new Guid("a1991ae3-1111-4865-ac00-4d73fef4cb3c");
        const int voteValue = 10;
        const int voteValueUpdated = 36;
        var vote = (Vote.Create(playerId, ticketId, voteValue).WithId(voteId) as Vote)!;

        DbContext.AddRange(game, ticket, vote);
        await DbContext.SaveChangesAsync();

        var command = new UpdateVoteCommand(vote.Id, voteValueUpdated);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.TicketId.Should().Be(ticketId);
        result.Value.Should().Be(voteValueUpdated);
    }
}