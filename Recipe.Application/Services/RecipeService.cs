using AutoMapper;
using Recipe.Application.Interfaces;
using Recipe.Application.Model.Response;
using Recipe.Domain;

namespace Recipe.Application.Services;

public class RecipeService(IRecipeRepository recipeRepository, IMapper mapper) : IRecipeService
{
    public async Task<IEnumerable<RecipeDto>> GetAllRecipes()
    {
        var recipes = await recipeRepository.GetAllRecipesAsync();
        return mapper.Map<IEnumerable<RecipeDto>>(recipes);
    }

    public async Task<RecipeDto> GetRecipeById(int id)
    {
        var recipe = await recipeRepository.GetRecipeByIdAsync(id)
                     ?? throw new KeyNotFoundException($"Recipe with ID {id} not found.");
        return mapper.Map<RecipeDto>(recipe);
    }

    public async Task<RecipeDto> UpdateRecipe(int id, RecipeDto recipeDto)
    {
        var entity = mapper.Map<Domain.Entities.Recipe>(recipeDto);
        entity.Id = id;

        var updated = await recipeRepository.UpdateRecipeAsync(entity)
                      ?? throw new KeyNotFoundException($"Recipe with ID {id} not found.");

        return mapper.Map<RecipeDto>(updated);
    }

    public async Task DeleteRecipeById(int id)
    {
        var recipe = await recipeRepository.GetRecipeByIdAsync(id)
                     ?? throw new KeyNotFoundException($"Recipe with ID {id} not found.");

        await recipeRepository.DeleteRecipeAsync(id);
    }

    public async Task<RecipeDto> CreateRecipe(RecipeDto recipeDto)
    {
        var recipe = mapper.Map<Domain.Entities.Recipe>(recipeDto);
        var created = await recipeRepository.CreateRecipeAsync(recipe);
        return mapper.Map<RecipeDto>(created);
    }

    public async Task<IEnumerable<RecipeDto>> FilterRecipesByTag(string tag)
    {
        var results = await recipeRepository.FilterRecipesByTagAsync(tag);
        return mapper.Map<IEnumerable<RecipeDto>>(results);
    }
}