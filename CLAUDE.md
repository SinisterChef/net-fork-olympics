# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Vision

Fork Olympics is a satirical food and cooking website — "The Onion meets America's Test Kitchen." The core joke is a parody of mom cooking blogs: ad-laden, SEO-bloated, padded with irrelevant backstories. Fork Olympics flips this by keeping the **actual cooking content legitimate** (real recipes, honest technique, light food science) while making the surrounding satire completely absurd.

Key satirical mechanics:
- **Recurring fictional authors** with consistent, ridiculous voices. Example: one author is a bear. The bear's Roasted Red Pepper Pasta article has a proper, detailed recipe — but the "background story" section is eight paragraphs of "Grrrr rrrroooorals." Every article by the bear is the same.
- **Parody of America's Test Kitchen** — serious-sounding food science breakdowns written by characters like a pirate who is confused why his scurvy went away after eating citrus.
- The verbose backstory sections that blogs use for SEO exist, but are played completely straight in absurd character voices.

Content types planned: recipes, review articles, food science/breakdown pieces, comments, user favorites, ratings. All content is author-attributed to a fictional character.

**Stack:** ASP.NET Core Blazor Server (.NET 10) backed by Supabase (PostgreSQL).

## Commands

All commands run from `ForkOlympics.Web/`.

```bash
dotnet run                        # Start dev server (https://localhost:7199)
dotnet build                      # Build only
dotnet watch                      # Hot-reload dev server

# EF Core migrations
dotnet ef migrations add <Name>   # Create a new migration
dotnet ef database update         # Apply pending migrations to Supabase
dotnet ef migrations remove       # Remove the last unapplied migration
```

## Secrets

The database connection string is stored in **User Secrets** (not appsettings). To set it locally:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<supabase-postgres-connection-string>"
```

The User Secrets ID is `86bbb4bf-6e22-41ba-ba43-be44f638cfdb`. `Program.cs` throws at startup if `DefaultConnection` is missing.

## Architecture

**Single project:** `ForkOlympics.Web/` — no separate API layer. Everything is Blazor Server with Interactive Server rendering, meaning all component logic runs on the server over a SignalR connection.

**Data layer:** `Data/` holds EF Core models and `ApplicationDbContext`. All database access goes through EF Core + Npgsql directly against Supabase's PostgreSQL endpoint. There is no Supabase SDK in use — Supabase is treated as plain PostgreSQL.

**Components:** `Components/Pages/` for routable pages, `Components/Layout/` for shell/nav. The root is `App.razor` → `Routes.razor` → `MainLayout.razor` wrapping page components.

**Rendering mode:** `AddInteractiveServerRenderMode()` is applied globally. Individual components can opt out with `@rendermode` directives if static rendering is preferred.

**Status code handling:** 404s re-execute to `/not-found` (not a redirect). Errors in production route to `/Error`.

## Database conventions

- Entity IDs are `Guid`, defaulted via `Guid.NewGuid()` in C# (not database-generated).
- Timestamps use `DateTime` with `UtcNow` default — keep all times in UTC.
- Migrations live in `Migrations/` and must be applied to Supabase manually via `dotnet ef database update` (no auto-migrate on startup).
