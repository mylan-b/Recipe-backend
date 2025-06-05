using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Recipe.Domain;
using Recipe.Infrastructure.Persistence;
using Recipe.Infrastructure.Repositories;

namespace Recipe.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information));
        return services;
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IRecipeRepository, RecipeRepository>();
    }
}