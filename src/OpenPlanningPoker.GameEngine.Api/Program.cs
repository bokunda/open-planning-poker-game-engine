var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration["ConnectionStrings:Database"]!)
    .AddCheck<CloudServiceHealthCheck>("CloudServiceProvider");

builder.Services.AddKeyCloak(builder.Configuration);
builder.Services.AddMemoryCache();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Host.UseSerilog();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

try
{
    Log.Information("Application Starting Up!");

    var app = builder
        .Build();

    if (app.Environment.IsDevelopment())
    {
        IdentityModelEventSource.ShowPII = true;
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.ApplyMigrations();

    app.UseHttpsRedirection();

    // Future improvements, this shouldn't be visible to everyone
    app.MapHealthChecks("/_health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    app.UseCustomExceptionHandler();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "The application failed to start correctly!");
    throw;
}
finally
{
    Log.CloseAndFlush();
}

namespace OpenPlanningPoker.GameEngine.Api
{
    public partial class Program { }
}
