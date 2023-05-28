using ExpenseTrackerBackend.Expenses;
using ExpenseTrackerBackend.Infrastructure;
using ExpenseTrackerBackend.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ExpenseTrackerDb")));

builder.Services.AddIdentity<User, Role>(opt =>
    {
        opt.Password.RequireDigit= false;
        opt.Password.RequireLowercase = false;
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequireUppercase = false;
        opt.Password.RequiredLength = 5;
    })
    .AddEntityFrameworkStores<ExpenseTrackerDbContext>();

builder.Services.AddAuthentication().AddCookie(opt =>
{
    opt.LoginPath = null;
    opt.LogoutPath = null;
});
builder.Services.AddAuthorization();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();


builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddControllers();

builder.Services.SetupCors(builder.Configuration);

var app = builder.Build();

// for simplicity - it would be a pipeline step
await RunMigrations(app.Services);

app.UseCors("cors");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();


app.MapExpensesApi();
app.MapControllers();

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

