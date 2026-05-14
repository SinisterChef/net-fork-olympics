using ForkOlympics.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace ForkOlympics.Web.Services;

public class AuthorService(ApplicationDbContext db)
{
    public async Task<List<Author>> GetAllAsync()
    {
        return await db.Authors.OrderBy(a => a.Name).ToListAsync();
    }

    public async Task<Author?> GetBySlugAsync(string slug)
    {
        return await db.Authors.FirstOrDefaultAsync(a => a.Slug == slug);
    }
}
