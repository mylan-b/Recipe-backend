using Microsoft.EntityFrameworkCore;
using Recipe.Domain;
using Recipe.Infrastructure.Handler;
using Recipe.Infrastructure.Persistence;

namespace Recipe.Infrastructure.Repositories;

public class RecipeRepository(ApplicationDbContext dbContext) : IRecipeRepository
{
    public async Task<IEnumerable<Domain.Entities.Recipe>> GetAllRecipesAsync()
    {
        return await dbContext.Recipe.ToListAsync();
    }

    public async Task<Domain.Entities.Recipe?> GetRecipeByIdAsync(int id)
    {
        var recipe = await dbContext.Recipe.FindAsync(id);
        if (recipe is null)
            ErrorHandler.NotFound("Recipe", id);
        return recipe;
    }

    public async Task<Domain.Entities.Recipe> CreateRecipeAsync(Domain.Entities.Recipe recipe)
    {
        var exists = await dbContext.Recipe.AnyAsync(r => r.Title.ToLower() == recipe.Title.ToLower());
        if (exists)
            ErrorHandler.AlreadyExists("Recipe", "Title", recipe.Title);
        
        dbContext.Recipe.Add(recipe);
        await dbContext.SaveChangesAsync();
        return recipe;
    }

    public async Task<Domain.Entities.Recipe?> UpdateRecipeAsync(Domain.Entities.Recipe recipe)
    {
        var existing = await dbContext.Recipe.FindAsync(recipe.Id);
        if (existing is null)
            ErrorHandler.NotFound("Recipe", recipe.Id);
        
        var duplicateTitle = await dbContext.Recipe
            .AnyAsync(r => r.Title.ToLower() == recipe.Title.ToLower() && r.Id != recipe.Id);

        if (duplicateTitle)
            ErrorHandler.AlreadyExists("Recipe", "Title", recipe.Title);

        dbContext.Entry(existing).CurrentValues.SetValues(recipe);
        await dbContext.SaveChangesAsync();
        return recipe;
    }

    public async Task DeleteRecipeAsync(int id)
    {
        var recipe = await dbContext.Recipe.FindAsync(id);
        if (recipe is null)
            ErrorHandler.NotFound("Recipe", id);

        dbContext.Recipe.Remove(recipe);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Recipe>> FilterRecipesByTagAsync(string tag)
    {
        return await dbContext.Recipe
            .Where(r => r.DietaryTag.Contains(tag))
            .ToListAsync();
    }
}