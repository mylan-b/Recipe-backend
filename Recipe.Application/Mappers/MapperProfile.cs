
using AutoMapper;
using Recipe.Application.Model.Response;

namespace Recipe.Application.Mappers;

public class MapperProfile: Profile
{
    public MapperProfile()
    {
        CreateMap<Domain.Entities.Recipe, RecipeDto>();
        CreateMap<Domain.Entities.Recipe, RecipeDto>().ReverseMap();
    }
}