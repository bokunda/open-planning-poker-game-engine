namespace OpenPlanningPoker.GameEngine.Infrastructure;

public sealed class OpenPlanningPokerGameEngineDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;
    private readonly IDateTimeProvider _dateTimeProvider;

    public OpenPlanningPokerGameEngineDbContext(
        DbContextOptions options,
        IDateTimeProvider dateTimeProvider, IPublisher publisher)
        : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OpenPlanningPokerGameEngineDbContext).Assembly);

        // Seed the data
        DbInitializer.SeedData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        try
        {
            SetCreateOnUpdateOn();

            var result = await base.SaveChangesAsync(cancellationToken);

            // Re-think, should publish domain events or save changes first? 
            // Because we can get exceptions in both ways.
            // Outbox pattern is a better approach
            await PublishDomainEventAsync();

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred!", ex);
        }
    }

    private void SetCreateOnUpdateOn()
    {
        var utcNow = _dateTimeProvider.UtcNow;
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is IEntityHasCreatedUpdated && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            ((IEntityHasCreatedUpdated)entityEntry.Entity).SetUpdated(utcNow);

            if (entityEntry.State == EntityState.Added)
            {
                ((IEntityHasCreatedUpdated)entityEntry.Entity).SetCreated(utcNow);
            }
        }
    }

    private async Task PublishDomainEventAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<IEntity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();

                // Important because we don't know what will happen inside specific domain event handlers
                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}