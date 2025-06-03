using Recipe.Application.Gateway.Model.Request;
using Recipe.Application.Interfaces;

namespace Recipe.Application.Services;

public class RecipeService : IRecipeService
{
    public Task<IEnumerable<RecipeDto>> GetAllRecipes()
    {
        throw new NotImplementedException();
    }

    public Task<RecipeDto> GetRecipeById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<RecipeDto> UpdateRecipe(int id, RecipeDto recipeDto)
    {
        throw new NotImplementedException();
    }

    public Task<RecipeDto> DeleteRecipeById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<RecipeDto> CreateRecipe(RecipeDto recipeDto)
    {
        throw new NotImplementedException();
    }
}