using AutoMapper;
using Moq;
using NUnit.Framework;
using Recipe.Application.Mappers;
using Recipe.Domain;

namespace Recipe.Tests;

public abstract class TestBase
{
    protected IMapper Mapper { get; private set; } = null!;
    protected Mock<IRecipeRepository> RecipeRepositoryMock { get; private set; } = null!;

    [SetUp]
    public void SetupBase()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperProfile>();
        });

        Mapper = config.CreateMapper();
        RecipeRepositoryMock = new Mock<IRecipeRepository>();
    }
}