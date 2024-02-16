namespace OpenPlanningPoker.GameEngine.Domain.UnitTests.Tickets;

public class TicketsTests : BaseTest
{
    [Fact]
    public void CreateTicket_Should_Raise_CreateTicketDomainEvent()
    {
        // Arrange
        var gameId = Guid.Parse("33a02402-abdc-484c-ae12-e908f99d7889");
        const string name = "Test Name";
        const string description = "Description Test";

        // Act
        var ticket = Ticket.Create(gameId, name, description);

        // Assert
        AssertDomainEventWasPublished<CreateTicketDomainEvent>(ticket);
        ticket.GameId.Should().Be(gameId);
        ticket.Name.Should().Be(name);
        ticket.Description.Should().Be(description);
    }

}