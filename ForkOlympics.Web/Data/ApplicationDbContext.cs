using Microsoft.EntityFrameworkCore;

namespace ForkOlympics.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Author> Authors => Set<Author>();
    public DbSet<ContentItem> ContentItems => Set<ContentItem>();
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();
    public DbSet<RecipeStep> RecipeSteps => Set<RecipeStep>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<ContentItemTag> ContentItemTags => Set<ContentItemTag>();
    public DbSet<MediaAsset> MediaAssets => Set<MediaAsset>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(e =>
        {
            e.HasIndex(a => a.Slug).IsUnique();
        });

        modelBuilder.Entity<ContentItem>(e =>
        {
            e.HasIndex(c => c.Slug).IsUnique();
            e.Property(c => c.Type).HasConversion<string>();
            e.HasOne(c => c.Author)
                .WithMany(a => a.ContentItems)
                .HasForeignKey(c => c.AuthorId);
        });

        modelBuilder.Entity<Recipe>(e =>
        {
            e.HasIndex(r => r.ContentItemId).IsUnique();
            e.HasOne(r => r.ContentItem)
                .WithOne(c => c.Recipe)
                .HasForeignKey<Recipe>(r => r.ContentItemId);
        });

        modelBuilder.Entity<RecipeIngredient>(e =>
        {
            e.HasOne(i => i.Recipe)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(i => i.RecipeId);
        });

        modelBuilder.Entity<RecipeStep>(e =>
        {
            e.HasOne(s => s.Recipe)
                .WithMany(r => r.Steps)
                .HasForeignKey(s => s.RecipeId);
        });

        modelBuilder.Entity<Tag>(e =>
        {
            e.HasIndex(t => t.Slug).IsUnique();
        });

        modelBuilder.Entity<ContentItemTag>(e =>
        {
            e.HasKey(ct => new { ct.ContentItemId, ct.TagId });
            e.HasOne(ct => ct.ContentItem)
                .WithMany(c => c.ContentItemTags)
                .HasForeignKey(ct => ct.ContentItemId);
            e.HasOne(ct => ct.Tag)
                .WithMany(t => t.ContentItemTags)
                .HasForeignKey(ct => ct.TagId);
        });

        modelBuilder.Entity<MediaAsset>(e =>
        {
            e.Property(m => m.Role).HasConversion<string>();
            e.HasOne(m => m.ContentItem)
                .WithMany(c => c.MediaAssets)
                .HasForeignKey(m => m.ContentItemId);
            e.HasOne(m => m.Step)
                .WithMany(s => s.MediaAssets)
                .HasForeignKey(m => m.StepId)
                .IsRequired(false);
        });
    }

    public override int SaveChanges()
    {
        StampUpdatedAt();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        StampUpdatedAt();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void StampUpdatedAt()
    {
        foreach (var entry in ChangeTracker.Entries<ContentItem>())
        {
            if (entry.State == EntityState.Modified)
                entry.Entity.UpdatedAtUtc = DateTime.UtcNow;
        }
    }
}
