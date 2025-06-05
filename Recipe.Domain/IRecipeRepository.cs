namespace Recipe.Domain;
using Recipe.Domain.Entities;
public interface IRecipeRepository
{
    Task<IEnumerable<Recipe>> GetAllRecipesAsync();
    Task<Recipe?> GetRecipeByIdAsync(int id);
    Task<Recipe> CreateRecipeAsync(Recipe recipe);
    Task<Recipe?> UpdateRecipeAsync(Recipe recipe);
    Task DeleteRecipeAsync(int id);
    Task<IEnumerable<Recipe>> FilterRecipesByTagAsync(string tag);
}