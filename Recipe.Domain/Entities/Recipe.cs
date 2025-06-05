using System.ComponentModel.DataAnnotations.Schema;

namespace Recipe.Domain.Entities;

public class Recipe
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Ingredients { get; set; } = default!;
    public string Steps { get; set; } = default!;
    public int CookingTimeMinutes { get; set; }
    public string DietaryTag { get; set; } = "";

    [NotMapped]
    public List<string> DietaryTags
    {
        get => string.IsNullOrEmpty(DietaryTag)
            ? []
            : DietaryTag.Split(',').ToList();

        set => DietaryTag = string.Join(",", value);
    }
}
