namespace ForkOlympics.Web.Data;

public class Author
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public string Slug { get; set; } = "";
    public string? Title { get; set; }
    public string? Tagline { get; set; }
    public string? Bio { get; set; }
    public string? AvatarStorageKey { get; set; }
    public string? Education { get; set; }
    public string? Experience { get; set; }
    public bool IsGuestAuthor { get; set; } = false;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public ICollection<ContentItem> ContentItems { get; set; } = [];
}
