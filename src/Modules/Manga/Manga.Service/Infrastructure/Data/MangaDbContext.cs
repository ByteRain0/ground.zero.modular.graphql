using Manga.Contracts.Models;
using Manga.Service.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace Manga.Service.Infrastructure.Data;

public class MangaDbContext(DbContextOptions<MangaDbContext> options)
    : DbContext(options)
{
    public DbSet<Contracts.Models.Manga> Mangas { get; set; }

    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(MangaDbContext).Assembly);
        builder.AddAuthorData();
        builder.AddMangaData();
    }
}
