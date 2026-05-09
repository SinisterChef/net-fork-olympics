namespace ForkOlympics.Web.Data;

public class Recipe
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = "";
    public string Slug { get; set; } = "";

    public string? Description { get; set; }

    public int PrepMinutes { get; set; }
    public int CookMinutes { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}