using ForkOlympics.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace ForkOlympics.Web.Services;

public class RecipeService(ApplicationDbContext db)
{
    public async Task<List<ContentItem>> GetPublishedAsync(string? tagSlug = null)
    {
        var query = db.ContentItems
            .Include(c => c.Author)
            .Include(c => c.Recipe)
            .Include(c => c.MediaAssets)
            .Include(c => c.ContentItemTags).ThenInclude(ct => ct.Tag)
            .Where(c => c.Type == ContentType.Recipe && c.IsPublished);

        if (tagSlug is not null)
            query = query.Where(c => c.ContentItemTags.Any(ct => ct.Tag.Slug == tagSlug));

        return await query.OrderByDescending(c => c.PublishedAt).ToListAsync();
    }

    public async Task<ContentItem?> GetBySlugAsync(string slug)
    {
        return await db.ContentItems
            .Include(c => c.Author)
            .Include(c => c.Recipe)
                .ThenInclude(r => r!.Ingredients.OrderBy(i => i.SortOrder))
            .Include(c => c.Recipe)
                .ThenInclude(r => r!.Steps.OrderBy(s => s.SortOrder))
            .Include(c => c.MediaAssets.OrderBy(m => m.SortOrder))
            .Include(c => c.ContentItemTags).ThenInclude(ct => ct.Tag)
            .FirstOrDefaultAsync(c => c.Slug == slug && c.Type == ContentType.Recipe);
    }
}
