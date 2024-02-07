namespace OpenPlanningPoker.GameEngine.Application.Abstractions.Clock;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}