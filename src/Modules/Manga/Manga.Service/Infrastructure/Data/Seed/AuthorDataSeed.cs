using Manga.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace Manga.Service.Infrastructure.Data.Seed;

internal static class AuthorDataSeed
{
    public static void AddAuthorData(this ModelBuilder builder)
    {
        builder.Entity<Author>().HasData(new List<Author>
        {
            new() { Id = 1, Name = "Eiichiro Oda" },
            new() { Id = 2, Name = "Masashi Kishimoto" },
            new() { Id = 3, Name = "Tite Kubo" },
            new() { Id = 4, Name = "Hajime Isayama" },
            new() { Id = 5, Name = "Akira Toriyama" },
            new() { Id = 6, Name = "Naoko Takeuchi" },
            new() { Id = 7, Name = "Yoshihiro Togashi" },
            new() { Id = 8, Name = "Kentaro Miura" },
            new() { Id = 9, Name = "Rumiko Takahashi" },
            new() { Id = 10, Name = "Clamp" }
        });
    }
}