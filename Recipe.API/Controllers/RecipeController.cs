using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Recipe.Application.Interfaces;
using Recipe.Application.Model.Response;

namespace Recipe.API.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/recipe/")]
public class RecipeController(IRecipeService recipeService) : ControllerBase
{
    [Produces("application/json")]
    [HttpGet("all", Name = "GetRecipes")]
    public async Task<IEnumerable<RecipeDto>> GetAllRecipes()
    {
        return await recipeService.GetAllRecipes();
    }

    [Produces("application/json")]
    [HttpGet("", Name = "GetARecipe")]
    public async Task<RecipeDto> GetRecipeById([FromHeader] int id)
    {
        return await recipeService.GetRecipeById(id);
    }

    [Produces("application/json")]
    [HttpDelete("", Name = "DeleteARecipe")]
    public async Task DeleteRecipeById([FromHeader] int id)
    {
        await recipeService.DeleteRecipeById(id);
    }

    [Produces("application/json")]
    [HttpPost("", Name = "CreateARecipe")]
    public async Task<RecipeDto> CreateRecipe([FromBody] RecipeDto recipeDto)
    {
        return await recipeService.CreateRecipe(recipeDto);
    }

    [Produces("application/json")]
    [HttpPut("", Name = "UpdateARecipe")]
    public async Task<RecipeDto> UpdateRecipe([FromHeader] int id, [FromBody] RecipeDto recipeDto)
    {
        return await recipeService.UpdateRecipe(id, recipeDto);
    }
    
    [HttpGet("filter", Name = "FilterByTag")]
    public async Task<IEnumerable<RecipeDto>> FilterByTag([FromQuery] string tag)
    {
        return await recipeService.FilterRecipesByTag(tag);
    }

}