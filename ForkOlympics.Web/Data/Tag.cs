namespace ForkOlympics.Web.Data;

public class Tag
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public string Slug { get; set; } = "";

    public ICollection<ContentItemTag> ContentItemTags { get; set; } = [];
}
