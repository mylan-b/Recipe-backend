using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recipe.Infrastructure.Persistence.Configuration;

public class RecipeConfig: IEntityTypeConfiguration<Domain.Entities.Recipe>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Recipe> builder)
    {
        builder.HasKey(r => r.Id);
        
        builder.HasData(
            new Domain.Entities.Recipe
            {
                Id = 1,
                Title = "Spaghetti Bolognese",
                Ingredients = "Spaghetti, Tomato Sauce, Beef",
                Steps = "Boil pasta, cook sauce, mix.",
                CookingTimeMinutes = 30,
                DietaryTag = "gluten,meat"
            },
            new Domain.Entities.Recipe
            {
                Id = 2,
                Title = "Vegan Tofu Stir Fry",
                Ingredients = "Tofu, Broccoli, Soy Sauce",
                Steps = "Fry tofu, add veg, stir-fry.",
                CookingTimeMinutes = 20,
                DietaryTag = "vegan,gluten-free"
            },
            new Domain.Entities.Recipe
            {
                Id = 3,
                Title = "Avocado Toast",
                Ingredients = "Bread, Avocado, Salt, Pepper",
                Steps = "Toast bread, spread avocado.",
                CookingTimeMinutes = 10,
                DietaryTag = "vegetarian"
            }
        );
    }
}