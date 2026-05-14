namespace ForkOlympics.Web.Data;

public class Recipe
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ContentItemId { get; set; }
    public string? Headnote { get; set; }
    public string? Backstory { get; set; }
    public string? WhyItWorks { get; set; }
    public string? Yield { get; set; }
    public int? PrepMinutes { get; set; }
    public int? CookMinutes { get; set; }
    public int? TotalMinutes { get; set; }

    public ContentItem ContentItem { get; set; } = null!;
    public ICollection<RecipeIngredient> Ingredients { get; set; } = [];
    public ICollection<RecipeStep> Steps { get; set; } = [];
}
