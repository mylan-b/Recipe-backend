using Microsoft.Extensions.DependencyInjection;
using Recipe.Application.Interfaces;
using Recipe.Application.Services;

namespace Recipe.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        RegisterApplicationServices(services);
        return services;
    }

    private static void RegisterApplicationServices(IServiceCollection services)
    {
        services.AddScoped<IRecipeService, RecipeService>();
    }
}