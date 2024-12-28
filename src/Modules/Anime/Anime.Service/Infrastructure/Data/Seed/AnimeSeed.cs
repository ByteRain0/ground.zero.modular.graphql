using Core.Clasifiers;
using Microsoft.EntityFrameworkCore;

namespace Anime.Service.Infrastructure.Data.Seed;

public static class AnimeSeed
{
    public static void AddAnimeData(this ModelBuilder builder)
    {
        builder.Entity<Contracts.Models.Anime>().HasData(new List<Contracts.Models.Anime>
        {
            // Studio Ghibli
            new() { Id = 1, Title = "My Neighbor Totoro", StudioId = 1, ReleaseDate = new DateTime(1988, 4, 16), Synopsis = "Two girls move to the countryside and encounter magical creatures.", Demographics = Demographics.Shoujo, TotalEpisodes = 1, IsCompleted = true, IsAiring = false },
            new() { Id = 2, Title = "Spirited Away", StudioId = 1, ReleaseDate = new DateTime(2001, 7, 20), Synopsis = "A young girl finds herself in a mysterious spirit world.", Demographics = Demographics.Shoujo, TotalEpisodes = 1, IsCompleted = true, IsAiring = false },

            // Toei Animation
            new() { Id = 3, Title = "Dragon Ball Z", StudioId = 2, ReleaseDate = new DateTime(1989, 4, 26), Synopsis = "The adventures of Goku as he defends Earth from powerful enemies.", Demographics = Demographics.Shounen, TotalEpisodes = 291, IsCompleted = true, IsAiring = false },
            new() { Id = 4, Title = "One Piece", StudioId = 2, ReleaseDate = new DateTime(1999, 10, 20), Synopsis = "A young pirate searches for the ultimate treasure.", Demographics = Demographics.Shounen, TotalEpisodes = 1000, IsCompleted = false, IsAiring = true },

            // Madhouse
            new() { Id = 5, Title = "Death Note", StudioId = 3, ReleaseDate = new DateTime(2006, 10, 3), Synopsis = "A high school student finds a notebook that allows him to kill anyone by writing their name.", Demographics = Demographics.Seinen, TotalEpisodes = 37, IsCompleted = true, IsAiring = false },
            new() { Id = 6, Title = "Hunter x Hunter", StudioId = 3, ReleaseDate = new DateTime(2011, 10, 2), Synopsis = "A young boy searches for his hunter father.", Demographics = Demographics.Shounen, TotalEpisodes = 148, IsCompleted = true, IsAiring = false },

            // Sunrise
            new() { Id = 7, Title = "Cowboy Bebop", StudioId = 4, ReleaseDate = new DateTime(1998, 4, 3), Synopsis = "A crew of bounty hunters chase criminals across the galaxy.", Demographics = Demographics.Seinen, TotalEpisodes = 26, IsCompleted = true, IsAiring = false },
            new() { Id = 8, Title = "Mobile Suit Gundam", StudioId = 4, ReleaseDate = new DateTime(1979, 4, 7), Synopsis = "A young pilot fights in a futuristic war with a giant robot suit.", Demographics = Demographics.Shounen, TotalEpisodes = 43, IsCompleted = true, IsAiring = false },

            // Bones
            new() { Id = 9, Title = "Fullmetal Alchemist: Brotherhood", StudioId = 5, ReleaseDate = new DateTime(2009, 4, 5), Synopsis = "Two brothers use alchemy to try to restore their bodies after a failed experiment.", Demographics = Demographics.Shounen, TotalEpisodes = 64, IsCompleted = true, IsAiring = false },
            new() { Id = 10, Title = "My Hero Academia", StudioId = 5, ReleaseDate = new DateTime(2016, 4, 3), Synopsis = "In a world where nearly everyone has superpowers, a powerless boy trains to be a hero.", Demographics = Demographics.Shounen, TotalEpisodes = 113, IsCompleted = false, IsAiring = true }
        });
    }
}