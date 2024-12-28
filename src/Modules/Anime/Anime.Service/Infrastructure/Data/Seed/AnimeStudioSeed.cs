using Anime.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace Anime.Service.Infrastructure.Data.Seed;

internal static class StudioDataSeed
{
    public static void AddStudioData(this ModelBuilder builder)
    {
        builder.Entity<Studio>().HasData(new List<Studio>
        {
            new() { Id = 1, Name = "Studio Ghibli" },
            new() { Id = 2, Name = "Toei Animation" },
            new() { Id = 3, Name = "Madhouse" },
            new() { Id = 4, Name = "Sunrise" },
            new() { Id = 5, Name = "Bones" }
        });
    }
}