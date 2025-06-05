using Recipe.Application.Model.Response;

namespace Recipe.Application.Interfaces;

public interface IRecipeService
{
    Task<IEnumerable<RecipeDto>> GetAllRecipes();
    Task<RecipeDto> GetRecipeById(int id);
    Task<RecipeDto> UpdateRecipe(int id, RecipeDto recipeDto);
    Task DeleteRecipeById(int id);
    Task<RecipeDto> CreateRecipe(RecipeDto recipeDto);
    Task<IEnumerable<RecipeDto>> FilterRecipesByTag(string tag);
}