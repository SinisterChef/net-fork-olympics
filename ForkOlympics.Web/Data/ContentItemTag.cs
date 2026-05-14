namespace ForkOlympics.Web.Data;

public class ContentItemTag
{
    public Guid ContentItemId { get; set; }
    public Guid TagId { get; set; }

    public ContentItem ContentItem { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}
