namespace OpenPlanningPoker.GameEngine.Infrastructure.Identity;

public class CurrentUserProvider : ICurrentUserProvider
{
    public Guid CustomerId { get; } = new(); // TODO: Implement this!
}