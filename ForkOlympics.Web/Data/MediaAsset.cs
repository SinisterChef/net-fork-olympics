namespace ForkOlympics.Web.Data;

public class MediaAsset
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ContentItemId { get; set; }
    public Guid? StepId { get; set; }
    public string StorageKey { get; set; } = "";
    public string? AltText { get; set; }
    public string? Caption { get; set; }
    public MediaRole Role { get; set; }
    public int SortOrder { get; set; }

    public ContentItem ContentItem { get; set; } = null!;
    public RecipeStep? Step { get; set; }
}
