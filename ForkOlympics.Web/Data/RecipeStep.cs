namespace ForkOlympics.Web.Data;

public class RecipeStep
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid RecipeId { get; set; }
    public string? SectionHeader { get; set; }
    public int SortOrder { get; set; }
    public string Body { get; set; } = "";

    public Recipe Recipe { get; set; } = null!;
    public ICollection<MediaAsset> MediaAssets { get; set; } = [];
}
