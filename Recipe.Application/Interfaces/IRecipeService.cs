using Recipe.Application.Gateway.Model.Request;

namespace Recipe.Application.Interfaces;

public interface IRecipeService
{
    Task<IEnumerable<RecipeDto>> GetAllRecipes();
    Task<RecipeDto> GetRecipeById(int id);
    Task<RecipeDto> UpdateRecipe(int id, RecipeDto recipeDto);
    Task<RecipeDto> DeleteRecipeById(int id);
    Task<RecipeDto> CreateRecipe(RecipeDto recipeDto);
}