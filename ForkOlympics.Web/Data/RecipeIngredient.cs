namespace ForkOlympics.Web.Data;

public class RecipeIngredient
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid RecipeId { get; set; }
    public string? GroupLabel { get; set; }
    public int SortOrder { get; set; }
    public string? Quantity { get; set; }
    public string? Unit { get; set; }
    public string Name { get; set; } = "";
    public string? PrepNote { get; set; }

    public Recipe Recipe { get; set; } = null!;
}
