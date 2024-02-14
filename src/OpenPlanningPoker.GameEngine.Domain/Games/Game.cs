namespace OpenPlanningPoker.GameEngine.Domain.Games;

public sealed class Game : Entity<Guid>
{
    private Game(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; }
    public string Description { get; }

    public GameSettings.GameSettings GameSettings => null!;
    public ICollection<GamePlayer.GamePlayer>? GamePlayers;
    public ICollection<Ticket>? Tickets;

    public static Game Create(string name, string description)
    {
        var game = new Game(name, description);
        game.RaiseDomainEvent(new CreateGameDomainEvent(game.Id));
        return game;
    }
}