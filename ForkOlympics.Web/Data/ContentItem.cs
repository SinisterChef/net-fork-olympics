namespace ForkOlympics.Web.Data;

public class ContentItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public ContentType Type { get; set; }
    public Guid AuthorId { get; set; }
    public string Slug { get; set; } = "";
    public string Title { get; set; } = "";
    public bool IsPublished { get; set; } = false;
    public DateTime? PublishedAt { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;

    public Author Author { get; set; } = null!;
    public Recipe? Recipe { get; set; }
    public ICollection<ContentItemTag> ContentItemTags { get; set; } = [];
    public ICollection<MediaAsset> MediaAssets { get; set; } = [];
}
