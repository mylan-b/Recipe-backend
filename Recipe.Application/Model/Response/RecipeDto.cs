using System.ComponentModel.DataAnnotations;

namespace Recipe.Application.Model.Response;

public class RecipeDto
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Ingredients { get; set; }

    [Required]
    public string Steps { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Cooking time must be greater than zero.")]
    public int CookingTimeMinutes { get; set; }

    [MinLength(1, ErrorMessage = "At least one dietary tag is required.")]
    public List<string> DietaryTags { get; set; }
}
