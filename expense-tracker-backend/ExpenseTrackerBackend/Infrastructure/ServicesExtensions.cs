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
            options.AddPolicy("cors", opt =>
            {
                opt.WithOrigins("http://localhost:5001", "http://127.0.0.1:5001")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }
}