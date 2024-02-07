namespace OpenPlanningPoker.GameEngine.Domain.Abstractions;

public interface IEntityHasCreatedUpdated
{
    void SetCreated(DateTimeOffset createdOn);
    void SetUpdated(DateTimeOffset updatedOn);
}