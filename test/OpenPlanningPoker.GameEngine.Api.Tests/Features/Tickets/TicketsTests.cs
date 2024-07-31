namespace OpenPlanningPoker.GameEngine.Api.Tests.Features.Tickets;

public class TicketsTests(ApiTestWebAppFactory factory) : BaseApiTests(factory)
{
    private readonly Game _firstGame = (Game.Create("GAME_1", "DESC_1").WithId(FirstGameId) as Game)!;
    private static readonly Guid FirstGameId = new("8ce78ab1-b52d-4650-9c57-568dc451022a");

    private readonly Ticket _firstTicket = (Ticket.Create(FirstGameId, "Ticket_1", "DESC_1").WithId(FirstTicketId) as Ticket)!;
    private static readonly Guid FirstTicketId = new ("c8fb7962-6e49-42ed-85f0-67b30bd48901");

    [Fact]
    public async Task GetTicket_Valid()
    {
        // Arrange
        DbContext.Add(_firstGame);
        DbContext.Add(_firstTicket);

        await DbContext.SaveChangesAsync();

        var query = new GetTicketQuery(FirstTicketId);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should().BeEquivalentTo(_firstTicket, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task GetTicket_TicketDoesNotExists()
    {
        // Arrange
        var query = new GetTicketQuery(Guid.Empty);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateTicket_Success()
    {
        // Arrange
        var command = new CreateTicketCommand(FirstGameId, "CREATE_Ticket", "TEST");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Should().BeEquivalentTo(command, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task UpdateTicket_Success()
    {
        // Arrange
        var createTicketCommand = new CreateTicketCommand(FirstGameId, "UPDATE_Ticket", "TEST");
        var createTicketResult = await Sender.Send(createTicketCommand);

        var updateTicketCommand = new UpdateTicketCommand(createTicketResult.Id, "UPDATE_Ticket_v2", "Test_v2");
        
        // Act
        var updateGameResult = Sender.Send(updateTicketCommand);

        // Assert
        updateGameResult.Should().BeEquivalentTo(updateTicketCommand, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ImportTicket_Success()
    {
        // Arrange
        var command = new ImportTicketsCommand(FirstGameId, new List<ImportTicketItem>
        {
            new ("Import_Ticket_1", "Description_1"),
            new ("Import_Ticket_2", "Description_2"),
            new ("Import_Ticket_3", "Description_3"),
        });

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Items.Should().BeEquivalentTo(command.Tickets, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task DeleteTicket_Success()
    {
        // Arrange
        var ticket = Ticket.Create(FirstGameId, "DELETE_Ticket", "TEST");
        DbContext.Add(ticket);
        await DbContext.SaveChangesAsync();

        var command = new DeleteTicketCommand(ticket.Id);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Should().NotBeNull();
    }
}