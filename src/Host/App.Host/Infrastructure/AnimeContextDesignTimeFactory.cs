using Anime.Service.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace App.Host.Infrastructure;

public sealed class AnimeContextDesignTimeFactory :
    IDesignTimeDbContextFactory<AnimeDbContext>
{
    public AnimeDbContext CreateDbContext(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);
    
        var postgres = builder
            .AddPostgres("postgres")
            .AddDatabase("anime-migrations", databaseName: "anime-migrations");

        var optionsBuilder = new DbContextOptionsBuilder<AnimeDbContext>();
        optionsBuilder.UseNpgsql("migrations");
        return new AnimeDbContext(optionsBuilder.Options);
    }
}