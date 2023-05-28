namespace ExpenseTrackerBackend.Infrastructure;

public static class ServicesExtensions
{
    public static void SetupCors(this IServiceCollection services, IConfiguration configuration)
    {
        var configurationSection = configuration.GetSection("AllowedOrigins");

        var allowedOrigins = configurationSection.Get<string>() ??
                             throw new InvalidOperationException("AllowedOrigins is not configured");
    
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(opt =>
            {
                opt.WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }
}