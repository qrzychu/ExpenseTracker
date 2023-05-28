using ExpenseTrackerBackend.Expenses;
using ExpenseTrackerBackend.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ExpenseTrackerDb")));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();


builder.Services.SetupCors(builder.Configuration);

var app = builder.Build();

// for simplicity - it would be a pipeline step
await RunMigrations(app.Services);

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.MapExpensesApi();

app.Run();

async Task RunMigrations(IServiceProvider services)
{
    using (var scope = services.CreateScope())
    {
        var dbContext = scope.ServiceProvider
            .GetRequiredService<ExpenseTrackerDbContext>();

        // Here is the migration executed
        await dbContext.Database.MigrateAsync();
    }
}

