using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForkOlympics.Web.Migrations
{
    /// <inheritdoc />
    public partial class SeedLemonChickenHeroImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                INSERT INTO "MediaAssets" ("Id", "ContentItemId", "StepId", "StorageKey", "AltText", "Caption", "Role", "SortOrder")
                SELECT gen_random_uuid(), ci."Id", NULL, 'https://mieisdchrrswuicqbfse.supabase.co/storage/v1/object/public/images/lemon-chicken.jpg', 'Classic Roasted Lemon Chicken on a platter', NULL, 'Hero', 0
                FROM "ContentItems" ci
                WHERE ci."Slug" = 'classic-roasted-lemon-chicken'
                AND NOT EXISTS (
                    SELECT 1 FROM "MediaAssets" ma
                    WHERE ma."ContentItemId" = ci."Id" AND ma."Role" = 'Hero'
                );
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DELETE FROM "MediaAssets" ma
                USING "ContentItems" ci
                WHERE ma."ContentItemId" = ci."Id"
                AND ci."Slug" = 'classic-roasted-lemon-chicken'
                AND ma."Role" = 'Hero';
                """);
        }
    }
}
