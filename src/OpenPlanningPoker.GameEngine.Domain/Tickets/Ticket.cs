namespace OpenPlanningPoker.GameEngine.Domain.Tickets;

public sealed class Ticket : Entity<Guid>
{
    public Guid GameId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Vote>? Votes;

    public Game Game { get; set; } = null!;

    internal Ticket(Guid gameId, string name, string description)
    {
        GameId = gameId;
        Name = name;
        Description = description;
    }

    public static Ticket Create(Guid gameId, string name, string description)
    {
        var ticket = new Ticket(gameId, name, description);
        ticket.RaiseDomainEvent(new CreateTicketDomainEvent(ticket.Id));
        return ticket;
    }
}