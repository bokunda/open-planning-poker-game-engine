namespace OpenPlanningPoker.GameEngine.Api.Tests.Extensions;

public static class DomainExtensions
{
    public static Entity<T> WithId<T>(this Entity<T> entity, T id)
    {
        entity.GetType().GetProperty(nameof(entity.Id))!.SetValue(entity, id);
        return entity;
    }
}