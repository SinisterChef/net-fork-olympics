using Microsoft.EntityFrameworkCore;

namespace ForkOlympics.Web.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db)
    {
        await SeedAuthorsAsync(db);
        await SeedRecipesAsync(db);
        await SeedPelmeniAsync(db);
        await SeedPancakesAsync(db);
        await SeedNewYorkPizzaAsync(db);
    }

    private static async Task SeedAuthorsAsync(ApplicationDbContext db)
    {
        var existingSlugs = await db.Authors.Select(a => a.Slug).ToHashSetAsync();

        var toAdd = new List<Author>();

        if (!existingSlugs.Contains("captain-barnabas-wentworth-iii"))
            toAdd.Add(new Author
            {
                Name         = "Captain Barnabas Wentworth III",
                Slug         = "captain-barnabas-wentworth-iii",
                Title        = "Senior Culinary Privateer",
                Tagline      = "Privateer. Nutritional Pioneer. Scurvy Survivor (Results Not Typical).",
                Bio          = "Captain Barnabas Wentworth III has sailed the seven seas for forty years, raided seventeen merchant vessels, and contracted scurvy on nine separate occasions — each of which he insists was a different disease. He came to cooking late in life, after a physician (a seagull he had befriended) suggested he eat more citrus. He ignored this advice for eleven years. He has since made up for lost time.",
                Education    = "Nine years aboard the HMS Perseverance under Captain Aldous Crick. One semester at the Lisbon Institute of Maritime Commerce, withdrawn after the incident with the harbormaster. The details of the incident are not relevant.",
                Experience   = "Forty years at sea across seven oceans. Culinary career began in 1689 following the involuntary acquisition of a lemon shipment from a merchant vessel outside Seville. Has since reconsidered his position on citrus in a profound and medically significant way. Senior Culinary Privateer at Fork Olympics since 2024.",
                IsGuestAuthor = false,
                CreatedAtUtc = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
            });

        if (!existingSlugs.Contains("barry-ursus"))
            toAdd.Add(new Author
            {
                Name         = "Barry Ursus",
                Slug         = "barry-ursus",
                Title        = "Contributing Omnivore",
                Tagline      = "Salmon. Honey. Berries. Sleep.",
                Bio          = "Barry Ursus is a 640-pound black bear residing in the Cascade Range of Washington State. He has no formal culinary training but has been eating for eleven years and considers this sufficient. His approach to flavor is instinctual, seasonal, and largely driven by what is currently running upstream. Barry's articles feature rigorously tested recipes with precise technique and accurate food science. His backstory sections are a different matter.",
                Education    = "Unstructured. Barry learned to forage, fish, and consume through eleven continuous years of field research in the Cascade Range. He considers this a more rigorous curriculum than most. He has also eaten a textbook. This was not intentional.",
                Experience   = "Eleven years of independent nutritional fieldwork with a focus on seasonal protein acquisition, high-calorie preparation methods, and not being seen by hikers. Contributing Omnivore at Fork Olympics since 2024. His recipe testing process is instinctual, thorough, and occasionally involves sitting in a river for several hours.",
                IsGuestAuthor = false,
                CreatedAtUtc = new DateTime(2024, 3, 2, 0, 0, 0, DateTimeKind.Utc)
            });

        if (!existingSlugs.Contains("vladimir-putin"))
            toAdd.Add(new Author
            {
                Name         = "Vladimir Putin",
                Slug         = "vladimir-putin",
                Title        = "President, Russian Federation. Also: Cook.",
                Tagline      = "Strong nation. Stronger stock.",
                Bio          = "Vladimir Putin has led the Russian Federation since 1999 and the Tuesday night dinner rotation at Novo-Ogaryovo since 2003. He approaches cooking the same way he approaches geopolitics: with patience, with conviction, and with a firm belief that anyone who uses pre-minced garlic from a jar is not to be trusted. His recipes skew heavily toward braised meats, root vegetables, and dishes that improve significantly after being left alone in a cold environment for several days. He will not be taking questions.",
                Education    = "Leningrad State University, Faculty of Law, 1975. Considers formal culinary education a symptom of softness. His grandmother taught him everything he needed to know at a kitchen table in Leningrad. He does not require a certificate to confirm this.",
                Experience   = "President of the Russian Federation since 1999. Head of the Tuesday night dinner rotation at Novo-Ogaryovo since 2003. Has made pelmeni every winter since the age of seven. His approach to recipe development mirrors his approach to statecraft: methodical, cold-resistant, and not open to feedback.",
                IsGuestAuthor = false,
                CreatedAtUtc = new DateTime(2024, 5, 9, 0, 0, 0, DateTimeKind.Utc)
            });

        if (!existingSlugs.Contains("raphael-turtle"))
            toAdd.Add(new Author
            {
                Name         = "Raphael Turtle",
                Slug         = "raphael-turtle",
                Title        = "Enforcer. Reluctant Chef.",
                Tagline      = "I don't do delicate. I do flavor.",
                Bio          = "Raphael has lived in the sewers beneath New York City his entire life and has strong opinions about this. He came to cooking not by choice but by necessity — Leonardo kept making the pasta wrong, Donatello was \"optimizing\" the sauce with a spreadsheet, and Michelangelo thought every meal should be pizza, which is correct but not sustainable. Raphael cooks with aggression and precision. His knife work is, by any objective measure, exceptional. He will not discuss where he learned it.",
                Education    = "Informal. Primarily self-taught under the constraints of sewer living and communal eating with three brothers who each had a different and incorrect opinion about food. Leonardo contributed unsolicited theory. Donatello contributed a spreadsheet. Michelangelo contributed enthusiasm without discipline. Raphael filtered these inputs and arrived at his own conclusions.",
                Experience   = "Six months of daily pizza production before abandoning the delivery grate entirely. Prior experience spans seventeen years of eating whatever was available in the greater Manhattan storm drain system. Currently the only member of his household who can properly deglaze a pan, julienne a vegetable, or make a roux without reading about it first. His knife skills were developed through a parallel career he declines to elaborate on.",
                IsGuestAuthor = false,
                CreatedAtUtc = new DateTime(2024, 7, 18, 0, 0, 0, DateTimeKind.Utc)
            });

        if (toAdd.Count > 0)
        {
            db.Authors.AddRange(toAdd);
            await db.SaveChangesAsync();
        }

        // Backfill new columns for authors already in the database
        var existing = await db.Authors.Where(a => a.Education == null).ToListAsync();
        foreach (var author in existing)
        {
            (author.Education, author.Experience) = author.Slug switch
            {
                "captain-barnabas-wentworth-iii" => (
                    "Nine years aboard the HMS Perseverance under Captain Aldous Crick. One semester at the Lisbon Institute of Maritime Commerce, withdrawn after the incident with the harbormaster. The details of the incident are not relevant.",
                    "Forty years at sea across seven oceans. Culinary career began in 1689 following the involuntary acquisition of a lemon shipment from a merchant vessel outside Seville. Has since reconsidered his position on citrus in a profound and medically significant way. Senior Culinary Privateer at Fork Olympics since 2024."
                ),
                "barry-ursus" => (
                    "Unstructured. Barry learned to forage, fish, and consume through eleven continuous years of field research in the Cascade Range. He considers this a more rigorous curriculum than most. He has also eaten a textbook. This was not intentional.",
                    "Eleven years of independent nutritional fieldwork with a focus on seasonal protein acquisition, high-calorie preparation methods, and not being seen by hikers. Contributing Omnivore at Fork Olympics since 2024. His recipe testing process is instinctual, thorough, and occasionally involves sitting in a river for several hours."
                ),
                "vladimir-putin" => (
                    "Leningrad State University, Faculty of Law, 1975. Considers formal culinary education a symptom of softness. His grandmother taught him everything he needed to know at a kitchen table in Leningrad. He does not require a certificate to confirm this.",
                    "President of the Russian Federation since 1999. Head of the Tuesday night dinner rotation at Novo-Ogaryovo since 2003. Has made pelmeni every winter since the age of seven. His approach to recipe development mirrors his approach to statecraft: methodical, cold-resistant, and not open to feedback."
                ),
                "raphael-turtle" => (
                    "Informal. Primarily self-taught under the constraints of sewer living and communal eating with three brothers who each had a different and incorrect opinion about food. Leonardo contributed unsolicited theory. Donatello contributed a spreadsheet. Michelangelo contributed enthusiasm without discipline. Raphael filtered these inputs and arrived at his own conclusions.",
                    "Six months of daily pizza production before abandoning the delivery grate entirely. Prior experience spans seventeen years of eating whatever was available in the greater Manhattan storm drain system. Currently the only member of his household who can properly deglaze a pan, julienne a vegetable, or make a roux without reading about it first. His knife skills were developed through a parallel career he declines to elaborate on."
                ),
                _ => (null, null)
            };
        }

        if (existing.Count > 0)
            await db.SaveChangesAsync();
    }

    private static async Task<Tag> GetOrCreateTagAsync(ApplicationDbContext db, string name, string slug)
    {
        var existing = await db.Tags.FirstOrDefaultAsync(t => t.Slug == slug);
        if (existing != null) return existing;
        var tag = new Tag { Name = name, Slug = slug };
        db.Tags.Add(tag);
        return tag;
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

        db.MediaAssets.Add(new MediaAsset
        {
            ContentItem = contentItem,
            StorageKey  = "https://mieisdchrrswuicqbfse.supabase.co/storage/v1/object/public/images/lemon-chicken.jpg",
            AltText     = "Classic Roasted Lemon Chicken on a platter",
            Role        = MediaRole.Hero,
            SortOrder   = 0
        });

        await db.SaveChangesAsync();
    }

    private static async Task SeedNewYorkPizzaAsync(ApplicationDbContext db)
    {
        if (await db.ContentItems.AnyAsync(c => c.Slug == "new-york-style-pizza"))
            return;

        var raphael = await db.Authors.FirstAsync(a => a.Slug == "raphael-turtle");

        // --- Tags ---

        var tagPizza    = await GetOrCreateTagAsync(db, "Pizza",    "pizza"   );
        var tagAmerican = await GetOrCreateTagAsync(db, "American", "american");

        // --- ContentItem ---

        var contentItem = new ContentItem
        {
            Type         = ContentType.Recipe,
            Author       = raphael,
            Slug         = "new-york-style-pizza",
            Title        = "New York-Style Pizza",
            IsPublished  = true,
            PublishedAt  = new DateTime(2024, 7, 22, 0, 0, 0, DateTimeKind.Utc),
            CreatedAtUtc = new DateTime(2024, 7, 22, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAtUtc = new DateTime(2024, 7, 22, 0, 0, 0, DateTimeKind.Utc)
        };

        db.ContentItems.Add(contentItem);

        db.ContentItemTags.AddRange(
            new ContentItemTag { ContentItem = contentItem, Tag = tagPizza    },
            new ContentItemTag { ContentItem = contentItem, Tag = tagAmerican }
        );

        // --- Recipe ---

        var recipe = new Recipe
        {
            ContentItem  = contentItem,
            Yield        = "Makes two 14-inch pizzas (serves 4 to 6)",
            PrepMinutes  = 30,
            CookMinutes  = 10,
            TotalMinutes = 40,

            Headnote = "New York-style pizza has two non-negotiable requirements: a thin, pliable crust that folds without cracking, and enough structural integrity to support its toppings without going limp. Both depend on the same variable — time. A cold ferment of at least 24 hours, preferably 48 to 72, develops the flavor compounds and extensible gluten structure that give the crust its characteristic chew and char. Rushing this step with extra yeast or a warm proof produces a crust that is edible but not correct. Oven temperature is equally non-negotiable: 500°F minimum, with a pizza steel or stone preheated for a full hour. If the bottom of the pizza does not hit extremely hot metal, you will not get the result you are looking for.",

            Backstory = @"We used to get our pizza through the grate on 6th and Bleecker. Sal's. Twenty-three years, same spot, same guy, same three pies on Friday night. Sal didn't ask questions. We didn't explain ourselves. It worked.

Then Sal retired. His son took over. His son does something he calls artisanal.

I'm not going to describe what artisanal means to someone who has never eaten a real slice of pizza in his life, but I will tell you that it involves a fig jam option and that I have strong feelings about this that I have expressed to no one because we live in a sewer and do not have standing to complain about restaurant quality.

April said we should make it ourselves. I told her that was not what we did. She brought the flour down anyway — left it by the ladder and didn't say anything, which is how April operates when she's already decided something. You don't win that argument. You just eventually start making pizza.

Leonardo had thoughts about the ferment schedule, which I did not ask for. Donatello built a spreadsheet to optimize the hydration ratio, which I threw in the storm drain on principle and then reconstructed from memory because he was right, which I told no one. Michelangelo wanted to eat the dough before it proofed. This is consistent with who Michelangelo is as a person.

I made the first one wrong. Too thick, wrong heat, crust didn't have any snap. The second one was better. By the time I'd been doing it for six months I stopped going to the grate at all. Not because I had to. Because mine was better than anything coming through that grate and I knew it and eventually everyone else knew it too, including Leonardo, who asked for the recipe and received no response.

I'm not going to tell you where I learned to stretch dough like this. Some things you keep.",

            WhyItWorks = @"The cold ferment accomplishes two things simultaneously. First, flavor: yeast and bacteria produce organic acids and aromatic compounds over 48 to 72 hours that fast-proofed doughs never develop. A same-day pizza dough tastes like flour and yeast. A three-day dough tastes like pizza. Second, extensibility: the gluten network immediately after kneading is tight and elastic and will spring back when you try to stretch it. Given time in the cold, it relaxes into something that can be pulled thin by hand without tearing. You must stretch by hand — rolling with a pin compresses the structure and pops the gas bubbles that have built up during the ferment. You will end up with a flat, dense crust, and you will have no one to blame but yourself.

Low-moisture whole-milk mozzarella is the correct cheese. Fresh mozzarella has too much water. It releases this water during baking, which pools on the surface and steams the crust from above instead of letting it crisp. Low-moisture mozzarella browns without flooding. Shred it yourself from a block — pre-shredded cheese is coated in cellulose to prevent clumping, which also prevents proper melting. The coating is fine in a salad. On a pizza, it is a problem.

The pizza steel replicates, imperfectly but adequately, the deck surface of a commercial pizza oven. Commercial ovens run at 700 to 900°F. Your home oven runs at 500 to 550°F. The steel compensates by storing and conducting heat more aggressively than any other surface a home cook has access to. Preheat it for a full hour at maximum temperature. It needs that time to reach thermal equilibrium through its entire mass. A stone works too. A sheet pan does not."
        };

        db.Recipes.Add(recipe);

        // --- Ingredients ---

        db.RecipeIngredients.AddRange(
            // Dough
            new RecipeIngredient { Recipe = recipe, SortOrder =  1, Quantity = "3",   Unit = "cups", Name = "bread flour",                  PrepNote = "plus more for dusting"         },
            new RecipeIngredient { Recipe = recipe, SortOrder =  2, Quantity = "1",   Unit = "tsp",  Name = "instant yeast"                                                              },
            new RecipeIngredient { Recipe = recipe, SortOrder =  3, Quantity = "1½",  Unit = "tsp",  Name = "kosher salt"                                                                },
            new RecipeIngredient { Recipe = recipe, SortOrder =  4, Quantity = "1",   Unit = "tsp",  Name = "granulated sugar"                                                           },
            new RecipeIngredient { Recipe = recipe, SortOrder =  5, Quantity = "2",   Unit = "tbsp", Name = "olive oil",                    PrepNote = "plus more for the bowl"        },
            new RecipeIngredient { Recipe = recipe, SortOrder =  6, Quantity = "1",   Unit = "cup",  Name = "warm water",                   PrepNote = "about 110°F"                   },
            // Sauce
            new RecipeIngredient { Recipe = recipe, SortOrder =  7, Quantity = "1",   Unit = "can",  Name = "whole San Marzano tomatoes",   PrepNote = "28 oz, crushed by hand"        },
            new RecipeIngredient { Recipe = recipe, SortOrder =  8, Quantity = "2",   Unit = null,   Name = "garlic cloves",                PrepNote = "minced"                        },
            new RecipeIngredient { Recipe = recipe, SortOrder =  9, Quantity = "1",   Unit = "tbsp", Name = "olive oil"                                                                  },
            new RecipeIngredient { Recipe = recipe, SortOrder = 10, Quantity = "½",   Unit = "tsp",  Name = "kosher salt"                                                                },
            new RecipeIngredient { Recipe = recipe, SortOrder = 11, Quantity = "¼",   Unit = "tsp",  Name = "red pepper flakes"                                                          },
            // Assembly
            new RecipeIngredient { Recipe = recipe, SortOrder = 12, Quantity = "8",   Unit = "oz",   Name = "low-moisture whole-milk mozzarella", PrepNote = "shredded from a block"   }
        );

        // --- Steps ---

        db.RecipeSteps.AddRange(
            new RecipeStep { Recipe = recipe, SortOrder = 1, Body = "Combine flour, yeast, salt, and sugar in a large bowl and whisk to combine. Add olive oil and warm water. Mix with a fork until a shaggy dough forms, then turn out onto an unfloured surface and knead for 8 to 10 minutes until smooth and only slightly tacky. The dough should spring back slowly when poked. Divide into two equal balls, coat lightly in olive oil, cover tightly, and refrigerate for 48 to 72 hours. 24 hours is the minimum. Less than that is a different recipe." },
            new RecipeStep { Recipe = recipe, SortOrder = 2, Body = "Make the sauce: heat olive oil in a small saucepan over medium heat. Add garlic and cook until fragrant, about 1 minute — do not brown it. Add hand-crushed tomatoes, salt, and red pepper flakes. Simmer uncovered for 15 minutes until slightly thickened. Taste and adjust salt. The sauce should be bright and slightly sharp; it will mellow in the oven. Let cool completely before using." },
            new RecipeStep { Recipe = recipe, SortOrder = 3, Body = "Two hours before baking, remove the dough balls from the refrigerator and let them come to room temperature, still covered. Cold dough will tear when stretched. Do not skip this." },
            new RecipeStep { Recipe = recipe, SortOrder = 4, Body = "Place a pizza steel or stone on the top rack of your oven and heat to 500°F (or as high as your oven goes). Allow it to preheat for a full hour. This is not a suggestion." },
            new RecipeStep { Recipe = recipe, SortOrder = 5, Body = "On a lightly floured surface, press one dough ball into a flat disc with your fingertips. Working from the center outward, use your knuckles to stretch the dough into a 14-inch round, rotating as you go. Let gravity do some of the work. If the dough tears, press it back together and let it rest 5 minutes before continuing. Do not use a rolling pin." },
            new RecipeStep { Recipe = recipe, SortOrder = 6, Body = "Transfer the stretched dough to a well-floured pizza peel or the back of a sheet pan. Spread a thin layer of sauce to within ½ inch of the edge. Distribute the mozzarella evenly. Work quickly — the longer the dough sits on the peel, the harder it becomes to launch." },
            new RecipeStep { Recipe = recipe, SortOrder = 7, Body = "Slide the pizza onto the hot steel. Bake until the crust is charred in spots and the cheese is bubbling and browned, 8 to 10 minutes. Remove with the peel, transfer to a wire rack, and let rest 2 minutes before slicing. A rested slice holds together. An immediate slice does not." },
            new RecipeStep { Recipe = recipe, SortOrder = 8, Body = "Slice into 8 pieces. Fold and eat. This is not optional — this is how you eat a New York slice. Flat is for people who don't know what they're doing." }
        );

        // --- Hero image ---

        db.MediaAssets.Add(new MediaAsset
        {
            ContentItem = contentItem,
            StorageKey  = "https://mieisdchrrswuicqbfse.supabase.co/storage/v1/object/public/images/pizza.webp",
            AltText     = "New York-style pizza with crispy, foldable crust",
            Role        = MediaRole.Hero,
            SortOrder   = 0
        });

        await db.SaveChangesAsync();
    }

    private static async Task SeedPancakesAsync(ApplicationDbContext db)
    {
        if (await db.ContentItems.AnyAsync(c => c.Slug == "fluffy-pancakes"))
            return;

        var barry = await db.Authors.FirstAsync(a => a.Slug == "barry-ursus");

        // --- Tags ---

        var tagBreakfast = await GetOrCreateTagAsync(db, "Breakfast", "breakfast");
        var tagAmerican  = await GetOrCreateTagAsync(db, "American",  "american" );

        // --- ContentItem ---

        var contentItem = new ContentItem
        {
            Type         = ContentType.Recipe,
            Author       = barry,
            Slug         = "fluffy-pancakes",
            Title        = "Fluffy Pancakes",
            IsPublished  = true,
            PublishedAt  = new DateTime(2024, 3, 8, 0, 0, 0, DateTimeKind.Utc),
            CreatedAtUtc = new DateTime(2024, 3, 8, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAtUtc = new DateTime(2024, 3, 8, 0, 0, 0, DateTimeKind.Utc)
        };

        db.ContentItems.Add(contentItem);

        db.ContentItemTags.AddRange(
            new ContentItemTag { ContentItem = contentItem, Tag = tagBreakfast },
            new ContentItemTag { ContentItem = contentItem, Tag = tagAmerican  }
        );

        // --- Recipe ---

        var recipe = new Recipe
        {
            ContentItem  = contentItem,
            Yield        = "Makes 10 to 12 pancakes (serves 3 to 4)",
            PrepMinutes  = 10,
            CookMinutes  = 20,
            TotalMinutes = 30,

            Headnote = "Fluffy pancakes depend on two things working in concert: sufficient leavening and minimal gluten development. Baking powder provides the primary lift; baking soda reacts with the acid in buttermilk for a secondary boost. The critical discipline is restraint when combining wet and dry ingredients — overmixed batter develops gluten strands that tighten the structure and produce flat, rubbery pancakes. A lumpy batter is the correct batter. A five-minute rest after mixing allows the leavening to activate and the flour to fully hydrate before anything touches the pan.",

            Backstory = @"Grrr.

Rrrr grr grr grr grrrr. Grr grr grr grr. Grrrrr grr grr grr grr grr grr. Grr grr. Rrrr. Grr grr grr grr grr grrrr grr grr grr grr grr. Grr grr grr. Grr.

Snff.

Grr grr grr grr grr grr grrrr. Grr. Rooaarrr. Grr grr grr grr grr grr grr grr grr grrrr. Grr grr grr grr grr. Sniff sniff. Grr grr grr grr. Grr grr grr grrrr grr grr grr grr.

Grr.

Rrrrrrr grr grr grr grr grr grr grr grr. Grr grr grr grr. Grr. Rrrr grr. Grr grr grr grr grr grrrr grr grr grr grr grr grr grr grr. Snff. Grr grr grr. Grr grr grrrr grr grr grr.

Rooooaaarrrr.

Grr grr grr grr grr grr. Grr. Grr grr grr grr grr grr grrrr grr grr. Rrrr. Grr grr grr grr grr grr. Hff. Grr grr grr grr grrrr grr grr grr grr grr grr. Grr grr. Grr.

Sniff.

Grr grr grr grr grr grrrr. Grr grr grr. Grr grr grr grr grr grr grr. Rrrr grr grr grr grr. Grr. Grr grr grr grr grrrr grr grr. Grr grr grr. Grrrrrrr.",

            WhyItWorks = @"The double leavening system — baking powder plus baking soda — creates two separate gas-release events. Baking powder reacts on contact with liquid, producing an initial rise in the batter. Baking soda reacts with the acid in the buttermilk and releases additional CO2 under heat, providing a second lift once the batter hits the pan. Together they produce a significantly taller pancake than either agent can achieve alone.

Buttermilk does more than activate the baking soda. Its mild acidity tenderizes the gluten strands, producing a more delicate crumb than whole milk would. The fat content also contributes to a richer flavor and helps the interior stay moist after cooking.

The bubbles that form across the surface of a cooking pancake are the structural indicator that the bottom is set and the interior is ready to be flipped. Flipping before those bubbles appear means flipping before the batter has solidified enough to hold its shape, which produces a collapsed, dense result. Wait for the bubbles. Wait for the edges to lose their shine. Flip once, cook briefly on the second side, and serve immediately — pancakes do not improve with sitting."
        };

        db.Recipes.Add(recipe);

        // --- Ingredients ---

        db.RecipeIngredients.AddRange(
            new RecipeIngredient { Recipe = recipe, SortOrder = 1, Quantity = "1½",  Unit = "cups", Name = "all-purpose flour"                                               },
            new RecipeIngredient { Recipe = recipe, SortOrder = 2, Quantity = "2",   Unit = "tbsp", Name = "granulated sugar"                                                },
            new RecipeIngredient { Recipe = recipe, SortOrder = 3, Quantity = "2",   Unit = "tsp",  Name = "baking powder"                                                   },
            new RecipeIngredient { Recipe = recipe, SortOrder = 4, Quantity = "½",   Unit = "tsp",  Name = "baking soda"                                                     },
            new RecipeIngredient { Recipe = recipe, SortOrder = 5, Quantity = "½",   Unit = "tsp",  Name = "kosher salt"                                                     },
            new RecipeIngredient { Recipe = recipe, SortOrder = 6, Quantity = "1¼",  Unit = "cups", Name = "buttermilk",          PrepNote = "room temperature"             },
            new RecipeIngredient { Recipe = recipe, SortOrder = 7, Quantity = "2",   Unit = null,   Name = "large eggs",          PrepNote = "room temperature"             },
            new RecipeIngredient { Recipe = recipe, SortOrder = 8, Quantity = "3",   Unit = "tbsp", Name = "unsalted butter",     PrepNote = "melted and cooled slightly"   },
            new RecipeIngredient { Recipe = recipe, SortOrder = 9, Quantity = "1",   Unit = "tsp",  Name = "vanilla extract"                                                 }
        );

        // --- Steps ---

        db.RecipeSteps.AddRange(
            new RecipeStep { Recipe = recipe, SortOrder = 1, Body = "Whisk flour, sugar, baking powder, baking soda, and salt together in a large bowl. Make a well in the center." },
            new RecipeStep { Recipe = recipe, SortOrder = 2, Body = "In a separate bowl, whisk buttermilk, eggs, melted butter, and vanilla until combined. Pour the wet ingredients into the well in the dry ingredients and fold together with a rubber spatula until just combined. The batter will be lumpy. This is correct. Do not attempt to smooth it." },
            new RecipeStep { Recipe = recipe, SortOrder = 3, Body = "Let the batter rest for 5 minutes without stirring. This allows the leavening to begin activating and the flour to hydrate fully." },
            new RecipeStep { Recipe = recipe, SortOrder = 4, Body = "Heat a nonstick skillet or griddle over medium heat. Add a small knob of butter and swirl to coat. When the foam subsides, the pan is ready. Pour ¼ cup of batter per pancake and cook until bubbles form across the entire surface and the edges look set and dry, 2 to 3 minutes." },
            new RecipeStep { Recipe = recipe, SortOrder = 5, Body = "Flip once and cook until the second side is golden brown, 1 to 2 minutes more. Do not press down on the pancakes. Do not flip them more than once. Transfer to a wire rack in a 200°F oven to keep warm while you cook remaining batches." },
            new RecipeStep { Recipe = recipe, SortOrder = 6, Body = "Serve immediately with butter and maple syrup. Pancakes held too long lose their lift and become dense. This is not a tragedy but it is preventable." }
        );

        // --- Hero image ---

        db.MediaAssets.Add(new MediaAsset
        {
            ContentItem = contentItem,
            StorageKey  = "https://mieisdchrrswuicqbfse.supabase.co/storage/v1/object/public/images/Fluffy-Homemade-Pancakes.jpg",
            AltText     = "Stack of fluffy homemade pancakes",
            Role        = MediaRole.Hero,
            SortOrder   = 0
        });

        await db.SaveChangesAsync();
    }

    private static async Task SeedPelmeniAsync(ApplicationDbContext db)
    {
        if (await db.ContentItems.AnyAsync(c => c.Slug == "pelmeni"))
            return;

        var putin = await db.Authors.FirstAsync(a => a.Slug == "vladimir-putin");

        // --- Tags ---

        var tagMainCourse = await GetOrCreateTagAsync(db, "Main Course", "main-course");
        var tagDumpling   = await GetOrCreateTagAsync(db, "Dumpling",    "dumpling"   );
        var tagRussian    = await GetOrCreateTagAsync(db, "Russian",      "russian"    );

        // --- ContentItem ---

        var contentItem = new ContentItem
        {
            Type         = ContentType.Recipe,
            Author       = putin,
            Slug         = "pelmeni",
            Title        = "Pelmeni",
            IsPublished  = true,
            PublishedAt  = new DateTime(2024, 5, 12, 0, 0, 0, DateTimeKind.Utc),
            CreatedAtUtc = new DateTime(2024, 5, 12, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAtUtc = new DateTime(2024, 5, 12, 0, 0, 0, DateTimeKind.Utc)
        };

        db.ContentItems.Add(contentItem);

        db.ContentItemTags.AddRange(
            new ContentItemTag { ContentItem = contentItem, Tag = tagMainCourse },
            new ContentItemTag { ContentItem = contentItem, Tag = tagDumpling   },
            new ContentItemTag { ContentItem = contentItem, Tag = tagRussian    }
        );

        // --- Recipe ---

        var recipe = new Recipe
        {
            ContentItem  = contentItem,
            Yield        = "Serves 4 to 6 (makes about 50 to 60 pelmeni)",
            PrepMinutes  = 60,
            CookMinutes  = 15,
            TotalMinutes = 75,

            Headnote = "Pelmeni require solving the same problem from two directions: a dough strong enough to survive boiling without becoming gummy, and a filling that stays juicy despite submersion in rapidly boiling water. The dough side is solved by resting the gluten after kneading, which allows thin rolling without the dough springing back. The filling side is solved by working cold water into the raw meat until it is slightly sticky — this keeps the fat emulsified through the cooking process rather than seizing into a dry, crumbled mass. Neither shortcut is acceptable.",

            Backstory = @"Pelmeni are not a recipe. They are a position.

I have made pelmeni every winter since I was seven years old. My grandmother made them at the kitchen table in Leningrad, and her grandmother before her, and so on back through generations who understood that food is not a comfort — it is a necessity, and a necessity done correctly is its own reward. Sentiment is for people who have not been cold.

The filling is simple. The dough is simple. The process is not simple. It requires patience, which I have in quantities that have surprised many people, and precision, which I have in quantities that have surprised fewer. You will fold each dumpling individually. You will seal each one completely. You will not rush this. A dumpling that opens in the pot is a failure of discipline, not of technique. Discipline and technique are the same thing.

I have eaten pelmeni in every condition a man can be in. I have eaten them at a properly set table and I have eaten them standing at a window in January, watching the city. I found the second occasion more satisfying. Context matters. Pelmeni eaten in cold and silence taste different than pelmeni eaten in warmth and company. I prefer the cold. This is not a complaint.

I will not tell you where I learned to make the dough this way. Some things a man keeps.",

            WhyItWorks = @"The dough is a simple flour-egg-water mixture, but its behavior is determined entirely by gluten development and rest time. Kneading aligns the gluten strands, which gives the dough strength. Resting relaxes them, which allows the dough to be rolled thin without springing back. Thin dough is non-negotiable. A thick pelmeni is a clumsy pelmeni. I have no use for clumsiness.

Cold water worked into the filling serves a specific function: it prevents the fat in the ground meat from separating during boiling. Meat is an emulsion of protein, fat, and water. Sustained heat disrupts this emulsion, which is why overcooked ground meat becomes dry and granular. The cold water, incorporated until the mixture is slightly sticky, creates a secondary emulsion that holds through the cooking process. The filling stays cohesive. It stays juicy. This is the correct outcome.

Pelmeni are done when they float to the surface and have cooked for an additional two to three minutes beyond that point. Floating indicates the interior has reached temperature and the gases have expanded. The additional time ensures the dough is cooked through and the filling is no longer pink. Remove them too early and the center is underdone. Leave them too long and the dough becomes soft and bloated. There is a correct moment. You will learn to recognize it, or you will make bad pelmeni for the rest of your life."
        };

        db.Recipes.Add(recipe);

        // --- Ingredients ---

        db.RecipeIngredients.AddRange(
            // Dough
            new RecipeIngredient { Recipe = recipe, SortOrder =  1, Quantity = "2",   Unit = "cups", Name = "all-purpose flour",  PrepNote = "plus more for dusting"    },
            new RecipeIngredient { Recipe = recipe, SortOrder =  2, Quantity = "1",   Unit = null,   Name = "large egg"                                                  },
            new RecipeIngredient { Recipe = recipe, SortOrder =  3, Quantity = "½",   Unit = "cup",  Name = "cold water"                                                 },
            new RecipeIngredient { Recipe = recipe, SortOrder =  4, Quantity = "1",   Unit = "tsp",  Name = "kosher salt"                                                },
            // Filling
            new RecipeIngredient { Recipe = recipe, SortOrder =  5, Quantity = "½",   Unit = "lb",   Name = "ground pork",        PrepNote = "cold"                     },
            new RecipeIngredient { Recipe = recipe, SortOrder =  6, Quantity = "½",   Unit = "lb",   Name = "ground beef (80/20)", PrepNote = "cold"                     },
            new RecipeIngredient { Recipe = recipe, SortOrder =  7, Quantity = "1",   Unit = null,   Name = "medium yellow onion", PrepNote = "finely grated"            },
            new RecipeIngredient { Recipe = recipe, SortOrder =  8, Quantity = "1",   Unit = "tsp",  Name = "kosher salt"                                                },
            new RecipeIngredient { Recipe = recipe, SortOrder =  9, Quantity = "½",   Unit = "tsp",  Name = "black pepper",        PrepNote = "freshly ground"           },
            new RecipeIngredient { Recipe = recipe, SortOrder = 10, Quantity = "2",   Unit = "tbsp", Name = "cold water"                                                 },
            // Serving
            new RecipeIngredient { Recipe = recipe, SortOrder = 11, Quantity = null,  Unit = null,   Name = "sour cream",          PrepNote = "for serving"              }
        );

        // --- Steps ---

        db.RecipeSteps.AddRange(
            new RecipeStep { Recipe = recipe, SortOrder = 1, Body = "Combine flour and salt in a large bowl. Make a well in the center and add the egg and cold water. Mix with a fork until a shaggy dough forms, then turn out onto a lightly floured surface and knead for 8 to 10 minutes until smooth and elastic. Wrap tightly in plastic and rest at room temperature for 30 minutes. Do not skip the rest." },
            new RecipeStep { Recipe = recipe, SortOrder = 2, Body = "Combine ground pork, ground beef, grated onion, salt, pepper, and cold water in a bowl. Mix with your hands for approximately 2 minutes until the mixture is uniform and slightly sticky. It should hold its shape when pressed. Refrigerate until you are ready to fill." },
            new RecipeStep { Recipe = recipe, SortOrder = 3, Body = "Divide the rested dough into 4 equal portions. Keep unused portions covered. Working with one portion at a time on a lightly floured surface, roll to roughly ⅛-inch thickness. Cut circles approximately 3 inches in diameter using a round cutter or the rim of a glass. Gather scraps and re-roll once." },
            new RecipeStep { Recipe = recipe, SortOrder = 4, Body = "Place 1 rounded teaspoon of filling in the center of each circle. Fold the dough over to form a half-moon and press the edges firmly to seal, working out any air pockets — an unsealed dumpling is an open failure. Bring the two corners of the half-moon together and pinch firmly to form the traditional pelmeni shape. Set on a lightly floured surface. Repeat with remaining dough and filling." },
            new RecipeStep { Recipe = recipe, SortOrder = 5, Body = "Bring a large pot of well-salted water to a rolling boil. Cook pelmeni in batches of 15 to 20, stirring once immediately after adding to prevent sticking. When they float to the surface, cook an additional 2 to 3 minutes. Remove with a slotted spoon." },
            new RecipeStep { Recipe = recipe, SortOrder = 6, Body = "Serve immediately with sour cream. White vinegar and a knob of unsalted butter are also traditional. They are options, not requirements. The pelmeni are sufficient on their own." }
        );

        // --- Hero image ---

        db.MediaAssets.Add(new MediaAsset
        {
            ContentItem = contentItem,
            StorageKey  = "https://mieisdchrrswuicqbfse.supabase.co/storage/v1/object/public/images/Pelemeni.jpg",
            AltText     = "Pelmeni served with sour cream",
            Role        = MediaRole.Hero,
            SortOrder   = 0
        });

        await db.SaveChangesAsync();
    }
}
