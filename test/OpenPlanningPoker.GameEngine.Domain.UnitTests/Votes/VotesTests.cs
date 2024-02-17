using OpenPlanningPoker.GameEngine.Domain.Votes;
using OpenPlanningPoker.GameEngine.Domain.Votes.Events;

namespace OpenPlanningPoker.GameEngine.Domain.UnitTests.Votes;

public class VotesTests : BaseTest
{
    [Fact]
    public void CreateVote_Should_Raise_CreateVoteDomainEvent()
    {
        // Arrange
        var playerId = Guid.Parse("33a02402-1111-484c-ae12-e908f99d7889");
        var ticketId = Guid.Parse("33a02402-2222-484c-ae12-e908f99d7889");
        const int value = 10;

        // Act
        var vote = Vote.Create(playerId, ticketId, value);

        // Assert
        AssertDomainEventWasPublished<CreateVoteDomainEvent>(vote);
        vote.PlayerId.Should().Be(playerId);
        vote.TicketId.Should().Be(ticketId);
        vote.Value.Should().Be(value);
    }

    [Fact]
    public void UpdateVote_Should_Raise_UpdateVoteDomainEvent()
    {
        // Arrange
        var playerId = Guid.Parse("33a02402-1111-484c-ae12-e908f99d7889");
        var ticketId = Guid.Parse("33a02402-2222-484c-ae12-e908f99d7889");
        const int value = 10;
        const int valueUpdated = 20;

        var vote = Vote.Create(playerId, ticketId, value);

        // Act
        vote.Update(valueUpdated);

        // Assert
        AssertDomainEventWasPublished<UpdateVoteDomainEvent>(vote);
        vote.PlayerId.Should().Be(playerId);
        vote.TicketId.Should().Be(ticketId);
        vote.Value.Should().Be(valueUpdated);
    }

}