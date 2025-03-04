using Microsoft.EntityFrameworkCore;
using Anime.Contracts.Models;
using Anime.Service.Infrastructure.Data.Seed;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Anime.Service.Infrastructure.Data;

/// <summary>
/// Left as public in order to allow having an external source run the migrations.
/// </summary>
/// <param name="options"></param>
public class AnimeDbContext(DbContextOptions<AnimeDbContext> options)
    : DbContext(options)
{
    public DbSet<Anime.Contracts.Models.Anime> Animes { get; set; }

    public DbSet<Studio> Studios { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasCollation("case_insensitive", locale: "en-u-ks-primary", provider: "icu", deterministic: false);
        builder.ApplyConfigurationsFromAssembly(typeof(AnimeDbContext).Assembly);
        builder.AddStudioData();
        builder.AddAnimeData();
    }
}

