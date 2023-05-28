using ExpenseTrackerBackend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ExpenseTrackerDb")));

var configurationSection = builder.Configuration.GetSection("AllowedOrigins");

Console.WriteLine(configurationSection.Value);

var allowedOrigins = configurationSection.Get<string>() ??
                     throw new InvalidOperationException("AllowedOrigins is not configured");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(opt =>
    {
        opt.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// for simplicity - it would be a pipeline step
await RunMigrations(app.Services);

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");
app.MapGet("/expenses", (ExpenseTrackerDbContext dbContext) => dbContext.Expenses.AsAsyncEnumerable())
    .WithName("GetExpenses");

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