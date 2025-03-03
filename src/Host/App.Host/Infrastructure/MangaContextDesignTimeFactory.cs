using Manga.Service.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace App.Host.Infrastructure;

public sealed class MangaContextDesignTimeFactory :
    IDesignTimeDbContextFactory<MangaDbContext>
{
    public MangaDbContext CreateDbContext(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var postgres = builder
            .AddPostgres("postgres")
            .AddDatabase("manga-migrations", databaseName: "manga-migrations");

        var optionsBuilder = new DbContextOptionsBuilder<MangaDbContext>();
        optionsBuilder.UseNpgsql("migrations");
        return new MangaDbContext(optionsBuilder.Options);
    }
}