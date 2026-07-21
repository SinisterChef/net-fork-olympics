using ForkOlympics.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace ForkOlympics.Web.Services;

public class RecipeService(IDbContextFactory<ApplicationDbContext> dbFactory)
{
    public async Task<List<ContentItem>> GetPublishedAsync(string? tagSlug = null, int? take = null)
    {
        await using var db = await dbFactory.CreateDbContextAsync();

        var query = db.ContentItems
            .Include(c => c.Author)
            .Include(c => c.Recipe)
            .Include(c => c.MediaAssets)
            .Include(c => c.ContentItemTags).ThenInclude(ct => ct.Tag)
            .Where(c => c.Type == ContentType.Recipe && c.IsPublished);

        if (tagSlug is not null)
            query = query.Where(c => c.ContentItemTags.Any(ct => ct.Tag.Slug == tagSlug));

        var ordered = query.OrderByDescending(c => c.PublishedAt);

        return take is not null
            ? await ordered.Take(take.Value).ToListAsync()
            : await ordered.ToListAsync();
    }

    public async Task<ContentItem?> GetBySlugAsync(string slug)
    {
        await using var db = await dbFactory.CreateDbContextAsync();

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
