using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetupAPI.Identity.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        return services;
    }
}