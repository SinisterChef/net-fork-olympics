using Microsoft.EntityFrameworkCore;

namespace ForkOlympics.Web.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	public DbSet<Recipe> Recipes => Set<Recipe>();
}