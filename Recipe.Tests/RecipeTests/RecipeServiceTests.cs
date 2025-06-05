using Moq;
using NUnit.Framework;
using Recipe.Application.Model.Response;
using Recipe.Application.Services;

namespace Recipe.Tests.RecipeTests;

[TestFixture]
public class RecipeServiceTests : TestBase
{
    private RecipeService _service = null!;

    [SetUp]
    public void SetUpService() =>
        _service = new RecipeService(RecipeRepositoryMock.Object, Mapper);

    [Test]
    public async Task GetAllRecipes_ReturnsAllRecipes()
    {
        RecipeRepositoryMock.Setup(r => r.GetAllRecipesAsync())
            .ReturnsAsync(new List<Domain.Entities.Recipe> { new() { Title = "Test", DietaryTag = "vegan" } });

        var result = await _service.GetAllRecipes();

        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().Title, Is.EqualTo("Test"));
    }

    [Test]
    public async Task GetRecipeById_ValidId_ReturnsRecipe()
    {
        RecipeRepositoryMock.Setup(r => r.GetRecipeByIdAsync(1))
            .ReturnsAsync(new Domain.Entities.Recipe { Id = 1, Title = "X", DietaryTag = "meat" });

        var result = await _service.GetRecipeById(1);

        Assert.That(result.Title, Is.EqualTo("X"));
    }

    [Test]
    public void GetRecipeById_InvalidId_Throws()
    {
        RecipeRepositoryMock.Setup(r => r.GetRecipeByIdAsync(99))
            .ReturnsAsync((Domain.Entities.Recipe?)null);

        Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetRecipeById(99));
    }

    [Test]
    public async Task CreateRecipe_Valid_ReturnsCreated()
    {
        var dto = new RecipeDto
        {
            Title = "CreateTest",
            Ingredients = "Stuff",
            Steps = "Cook it",
            CookingTimeMinutes = 10,
            DietaryTags = new List<string> { "gluten" }
        };

        RecipeRepositoryMock.Setup(r => r.CreateRecipeAsync(It.IsAny<Domain.Entities.Recipe>()))
            .ReturnsAsync((Domain.Entities.Recipe r) => r);

        var result = await _service.CreateRecipe(dto);

        Assert.That(result.Title, Is.EqualTo("CreateTest"));
        Assert.That(result.DietaryTags, Contains.Item("gluten"));
    }

    [Test]
    public async Task UpdateRecipe_ValidId_ReturnsUpdated()
    {
        var dto = new RecipeDto
        {
            Title = "Updated",
            Ingredients = "i",
            Steps = "s",
            CookingTimeMinutes = 5,
            DietaryTags = new List<string> { "low-fat" }
        };

        RecipeRepositoryMock.Setup(r => r.UpdateRecipeAsync(It.IsAny<Domain.Entities.Recipe>()))
            .ReturnsAsync((Domain.Entities.Recipe r) => r);

        var result = await _service.UpdateRecipe(1, dto);

        Assert.That(result.Title, Is.EqualTo("Updated"));
    }

    [Test]
    public void UpdateRecipe_InvalidId_Throws()
    {
        RecipeRepositoryMock.Setup(r => r.UpdateRecipeAsync(It.IsAny<Domain.Entities.Recipe>()))
            .ReturnsAsync((Domain.Entities.Recipe?)null);

        var dto = new RecipeDto { Title = "Nope", Ingredients = "x", Steps = "y", CookingTimeMinutes = 1, DietaryTags = new() { "a" } };

        Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateRecipe(999, dto));
    }

    [Test]
    public void DeleteRecipeById_InvalidId_Throws()
    {
        RecipeRepositoryMock.Setup(r => r.GetRecipeByIdAsync(99)).ReturnsAsync((Domain.Entities.Recipe?)null);

        Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteRecipeById(99));
    }

    [Test]
    public async Task FilterRecipesByTag_ReturnsMatching()
    {
        var recipes = new List<Domain.Entities.Recipe>
        {
            new() { Title = "A", DietaryTag = "vegan,gluten" },
            new() { Title = "B", DietaryTag = "vegetarian" }
        };

        RecipeRepositoryMock.Setup(r => r.FilterRecipesByTagAsync("vegan"))
            .ReturnsAsync(recipes.Where(r => r.DietaryTag.Contains("vegan")).ToList());

        var result = await _service.FilterRecipesByTag("vegan");

        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().Title, Is.EqualTo("A"));
    }
}