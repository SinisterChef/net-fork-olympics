using Microsoft.EntityFrameworkCore;

namespace ForkOlympics.Web.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db)
    {
        await SeedAuthorsAsync(db);
        await SeedRecipesAsync(db);
    }

    private static async Task SeedAuthorsAsync(ApplicationDbContext db)
    {
        var existingSlugs = await db.Authors.Select(a => a.Slug).ToHashSetAsync();

        var toAdd = new List<Author>();

        if (!existingSlugs.Contains("captain-barnabas-wentworth-iii"))
            toAdd.Add(new Author
            {
                Name     = "Captain Barnabas Wentworth III",
                Slug     = "captain-barnabas-wentworth-iii",
                Title    = "Senior Culinary Privateer",
                Tagline  = "Privateer. Nutritional Pioneer. Scurvy Survivor (Results Not Typical).",
                Bio      = "Captain Barnabas Wentworth III has sailed the seven seas for forty years, raided seventeen merchant vessels, and contracted scurvy on nine separate occasions — each of which he insists was a different disease. He came to cooking late in life, after a physician (a seagull he had befriended) suggested he eat more citrus. He ignored this advice for eleven years. He has since made up for lost time.",
                CreatedAtUtc = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
            });

        if (!existingSlugs.Contains("barry-ursus"))
            toAdd.Add(new Author
            {
                Name     = "Barry Ursus",
                Slug     = "barry-ursus",
                Title    = "Contributing Omnivore",
                Tagline  = "Salmon. Honey. Berries. Sleep.",
                Bio      = "Barry Ursus is a 640-pound black bear residing in the Cascade Range of Washington State. He has no formal culinary training but has been eating for eleven years and considers this sufficient. His approach to flavor is instinctual, seasonal, and largely driven by what is currently running upstream. Barry's articles feature rigorously tested recipes with precise technique and accurate food science. His backstory sections are a different matter.",
                CreatedAtUtc = new DateTime(2024, 3, 2, 0, 0, 0, DateTimeKind.Utc)
            });

        if (!existingSlugs.Contains("vladimir-putin"))
            toAdd.Add(new Author
            {
                Name     = "Vladimir Putin",
                Slug     = "vladimir-putin",
                Title    = "President, Russian Federation. Also: Cook.",
                Tagline  = "Strong nation. Stronger stock.",
                Bio      = "Vladimir Putin has led the Russian Federation since 1999 and the Tuesday night dinner rotation at Novo-Ogaryovo since 2003. He approaches cooking the same way he approaches geopolitics: with patience, with conviction, and with a firm belief that anyone who uses pre-minced garlic from a jar is not to be trusted. His recipes skew heavily toward braised meats, root vegetables, and dishes that improve significantly after being left alone in a cold environment for several days. He will not be taking questions.",
                CreatedAtUtc = new DateTime(2024, 5, 9, 0, 0, 0, DateTimeKind.Utc)
            });

        if (!existingSlugs.Contains("raphael-turtle"))
            toAdd.Add(new Author
            {
                Name     = "Raphael Turtle",
                Slug     = "raphael-turtle",
                Title    = "Enforcer. Reluctant Chef.",
                Tagline  = "I don't do delicate. I do flavor.",
                Bio      = "Raphael has lived in the sewers beneath New York City his entire life and has strong opinions about this. He came to cooking not by choice but by necessity — Leonardo kept making the pasta wrong, Donatello was \"optimizing\" the sauce with a spreadsheet, and Michelangelo thought every meal should be pizza, which is correct but not sustainable. Raphael cooks with aggression and precision. His knife work is, by any objective measure, exceptional. He will not discuss where he learned it.",
                CreatedAtUtc = new DateTime(2024, 7, 18, 0, 0, 0, DateTimeKind.Utc)
            });

        if (toAdd.Count > 0)
        {
            db.Authors.AddRange(toAdd);
            await db.SaveChangesAsync();
        }
    }

    private static async Task SeedRecipesAsync(ApplicationDbContext db)
    {
        if (await db.ContentItems.AnyAsync(c => c.Slug == "classic-roasted-lemon-chicken"))
            return;

        var captain = await db.Authors.FirstAsync(a => a.Slug == "captain-barnabas-wentworth-iii");

        // --- Tags ---

        var tagChicken    = new Tag { Name = "Chicken",     Slug = "chicken"      };
        var tagMainCourse = new Tag { Name = "Main Course", Slug = "main-course"  };
        var tagWeeknight  = new Tag { Name = "Weeknight",   Slug = "weeknight"    };
        var tagRoasted    = new Tag { Name = "Roasted",     Slug = "roasted"      };

        db.Tags.AddRange(tagChicken, tagMainCourse, tagWeeknight, tagRoasted);

        // --- ContentItem ---

        var contentItem = new ContentItem
        {
            Type         = ContentType.Recipe,
            Author       = captain,
            Slug         = "classic-roasted-lemon-chicken",
            Title        = "Classic Roasted Lemon Chicken",
            IsPublished  = true,
            PublishedAt  = new DateTime(2024, 1, 20, 0, 0, 0, DateTimeKind.Utc),
            CreatedAtUtc = new DateTime(2024, 1, 20, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAtUtc = new DateTime(2024, 1, 20, 0, 0, 0, DateTimeKind.Utc)
        };

        db.ContentItems.Add(contentItem);

        db.ContentItemTags.AddRange(
            new ContentItemTag { ContentItem = contentItem, Tag = tagChicken    },
            new ContentItemTag { ContentItem = contentItem, Tag = tagMainCourse },
            new ContentItemTag { ContentItem = contentItem, Tag = tagWeeknight  },
            new ContentItemTag { ContentItem = contentItem, Tag = tagRoasted    }
        );

        // --- Recipe ---

        var recipe = new Recipe
        {
            ContentItem  = contentItem,
            Yield        = "Serves 4",
            PrepMinutes  = 15,
            CookMinutes  = 75,
            TotalMinutes = 90,

            Headnote = "Achieving the perfect roast chicken requires addressing two competing problems: the breast meat, which dries out quickly, and the thighs, which need more time to render their fat and connective tissue. Salting the bird at least an hour ahead seasons it deeply and helps retain moisture during roasting, while high heat and a well-dried skin are non-negotiable for achieving the crackle that makes this dish worth making.",

            Backstory = @"'Twas the winter of 1689 when I first laid eyes upon a lemon, and I'll confess to ye now that I did not trust it. Yellow and round it was, like a tiny cursed sun pulled from the sea. I had at that point lost four of me teeth and most of me ability to feel me fingers — conditions I had come to accept as simply the pirate lifestyle. Me physician (a seagull I had befriended who seemed judgmental) had suggested I eat more citrus. I ignored him.

The chicken, however — the chicken changed everything. I had acquired it whilst relieving a merchant vessel of its cargo outside Seville. The merchants had lemons. Naturally I used these lemons to threaten them. When they surrendered, I took the chicken and the lemons. Having nothing else to cook with, I roasted the chicken with the lemons inside.

Three days later me teeth stopped being loose.

I did not connect these events for eleven years.

When I finally made the connection, I wept. Not because of the suffering I could have avoided, but because I had been threatening people with medicine this whole time and it had still worked. What does that say about me? What does that say about citrus? What does that say about threats?

This recipe is the result of forty years of refinement, seventeen cases of scurvy (nine of which were definitely a different disease), and one very judgmental seagull. I present it to ye now in the hopes that ye may learn from me journey, or at least eat a very good chicken.",

            WhyItWorks = @"The lemon, when exposed to heat, releases its oils and acids into the surrounding cavity. These compounds — citric acid, limonene, various other things the apothecary could not name to my satisfaction — perfume the meat from the inside as the bird roasts. The vitamin C content of the lemon, of which I have strong and complicated feelings, does not survive high heat, but has already done its work on the cook during preparation, assuming the cook aggressively samples the lemons before they go in. I do. Every time.

Salting the bird ahead of time draws moisture to the surface through osmosis, which then gets reabsorbed carrying the salt deeper into the meat. This is called brining from the inside, and it is far more civilized than the brine barrels we used to store meat aboard ship, which I will not describe here.

The high roasting temperature (425°F) drives rapid moisture evaporation from the skin, allowing the Maillard reaction to develop the brown, complex flavors that lesser preparations lack. The resting period after cooking allows the muscle fibers, which have contracted under heat, to relax and reabsorb the juices that have been driven toward the center. Cut too early and those juices run onto the board. I have made this mistake. It haunts me."
        };

        db.Recipes.Add(recipe);

        // --- Ingredients ---

        db.RecipeIngredients.AddRange(
            new RecipeIngredient { Recipe = recipe, SortOrder = 1, Quantity = "1",   Unit = null,   Name = "whole chicken (3½–4 lbs)",  PrepNote = "patted completely dry with paper towels" },
            new RecipeIngredient { Recipe = recipe, SortOrder = 2, Quantity = "2",   Unit = "tsp",  Name = "kosher salt"                                                                      },
            new RecipeIngredient { Recipe = recipe, SortOrder = 3, Quantity = "1",   Unit = "tsp",  Name = "black pepper",              PrepNote = "freshly ground"                          },
            new RecipeIngredient { Recipe = recipe, SortOrder = 4, Quantity = "1",   Unit = null,   Name = "lemon",                     PrepNote = "halved"                                  },
            new RecipeIngredient { Recipe = recipe, SortOrder = 5, Quantity = "4",   Unit = null,   Name = "garlic cloves",             PrepNote = "smashed"                                 },
            new RecipeIngredient { Recipe = recipe, SortOrder = 6, Quantity = "4",   Unit = null,   Name = "fresh thyme sprigs"                                                              },
            new RecipeIngredient { Recipe = recipe, SortOrder = 7, Quantity = "2",   Unit = "tbsp", Name = "unsalted butter",           PrepNote = "softened"                                },
            new RecipeIngredient { Recipe = recipe, SortOrder = 8, Quantity = "1",   Unit = "tbsp", Name = "olive oil"                                                                       }
        );

        // --- Steps ---

        db.RecipeSteps.AddRange(
            new RecipeStep { Recipe = recipe, SortOrder = 1, Body = "Adjust oven rack to the middle position and heat oven to 425°F. Pat the chicken completely dry with paper towels — surface moisture is the enemy of crispy skin and will not be tolerated. Season generously all over with salt and pepper, including inside the cavity." },
            new RecipeStep { Recipe = recipe, SortOrder = 2, Body = "Stuff the cavity with the lemon halves, smashed garlic, and thyme sprigs. Tuck the wing tips behind the back of the bird to prevent them from burning and to help the breast sit more evenly." },
            new RecipeStep { Recipe = recipe, SortOrder = 3, Body = "Combine softened butter and olive oil in a small bowl. Rub the mixture evenly over the entire outside of the chicken, working your fingers under the breast skin to coat the meat directly where possible. This is important. Do not skip this." },
            new RecipeStep { Recipe = recipe, SortOrder = 4, Body = "Place the chicken breast-side up in a roasting pan or oven-safe skillet. Roast until an instant-read thermometer inserted into the thickest part of the thigh (avoiding bone) registers 165°F, 60–75 minutes. The skin should be deep golden brown and crackling." },
            new RecipeStep { Recipe = recipe, SortOrder = 5, Body = "Transfer the chicken to a cutting board and let rest, uncovered, for 15 minutes before carving. Resting is not optional. It allows the juices to redistribute throughout the meat. Carving too early will result in a dry bird and a wet cutting board, and ye will have no one to blame but yourself." }
        );

        await db.SaveChangesAsync();
    }
}
