using ForkOlympics.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace ForkOlympics.Web.Services;

public class AuthorService(IDbContextFactory<ApplicationDbContext> dbFactory)
{
    public async Task<List<Author>> GetAllAsync()
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        return await db.Authors.OrderBy(a => a.Name).ToListAsync();
    }

    public async Task<Author?> GetBySlugAsync(string slug)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        return await db.Authors
            .Include(a => a.ContentItems.Where(c => c.IsPublished && c.Type == ContentType.Recipe))
                .ThenInclude(c => c.MediaAssets)
            .Include(a => a.ContentItems.Where(c => c.IsPublished && c.Type == ContentType.Recipe))
                .ThenInclude(c => c.ContentItemTags).ThenInclude(ct => ct.Tag)
            .FirstOrDefaultAsync(a => a.Slug == slug);
    }
}
