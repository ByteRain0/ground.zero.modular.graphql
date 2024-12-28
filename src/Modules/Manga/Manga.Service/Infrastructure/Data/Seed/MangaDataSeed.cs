using Core.Clasifiers;
using Microsoft.EntityFrameworkCore;

namespace Manga.Service.Infrastructure.Data.Seed;

internal static class MangaDataSeed
{
    public static void AddMangaData(this ModelBuilder builder)
    {
        var mangas = new List<Contracts.Models.Manga>
        {
            // Eiichiro Oda
            new() { Id = 1, Title = "One Piece", ReleaseDate = new DateTime(1997, 7, 22), Synopsis = "A young pirate searches for the ultimate treasure.", Demographics = Demographics.Shounen, TotalChapters = 1000, IsCompleted = false, IsAiring = true, AuthorId = 1 },
            
            // Masashi Kishimoto
            new() { Id = 2, Title = "Naruto", ReleaseDate = new DateTime(1999, 9, 21), Synopsis = "A ninja seeks recognition and dreams of becoming the Hokage.", Demographics = Demographics.Shounen, TotalChapters = 700, IsCompleted = true, IsAiring = false, AuthorId = 2 },
            new() { Id = 3, Title = "Boruto: Naruto Next Generations", ReleaseDate = new DateTime(2016, 5, 9), Synopsis = "The next generation of ninjas, including Naruto's son, embarks on new adventures.", Demographics = Demographics.Shounen, TotalChapters = 63, IsCompleted = false, IsAiring = true, AuthorId = 2 },
            
            // Tite Kubo
            new() { Id = 4, Title = "Bleach", ReleaseDate = new DateTime(2001, 8, 7), Synopsis = "A teenager gains soul reaper powers to protect the living and dead.", Demographics = Demographics.Shounen, TotalChapters = 686, IsCompleted = true, IsAiring = false, AuthorId = 3 },
            new() { Id = 5, Title = "Burn the Witch", ReleaseDate = new DateTime(2018, 7, 16), Synopsis = "Two witches protect and control dragons in a parallel world.", Demographics = Demographics.Seinen, TotalChapters = 5, IsCompleted = true, IsAiring = false, AuthorId = 3 },
            
            // Hajime Isayama
            new() { Id = 6, Title = "Attack on Titan", ReleaseDate = new DateTime(2009, 9, 9), Synopsis = "Humanity battles titans for survival within walls.", Demographics = Demographics.Seinen, TotalChapters = 139, IsCompleted = true, IsAiring = false, AuthorId = 4 },
            
            // Akira Toriyama
            new() { Id = 7, Title = "Dragon Ball", ReleaseDate = new DateTime(1984, 12, 3), Synopsis = "A young martial artist embarks on adventures to find dragon balls.", Demographics = Demographics.Shounen, TotalChapters = 519, IsCompleted = true, IsAiring = false, AuthorId = 5 },
            new() { Id = 8, Title = "Dr. Slump", ReleaseDate = new DateTime(1980, 1, 1), Synopsis = "The adventures of a brilliant but quirky inventor and his robot girl.", Demographics = Demographics.Shounen, TotalChapters = 236, IsCompleted = true, IsAiring = false, AuthorId = 5 },
            
            // Naoko Takeuchi
            new() { Id = 9, Title = "Sailor Moon", ReleaseDate = new DateTime(1991, 12, 28), Synopsis = "A teenage girl transforms into a warrior to save the world.", Demographics = Demographics.Shoujo, TotalChapters = 60, IsCompleted = true, IsAiring = false, AuthorId = 6 },
            new() { Id = 10, Title = "Codename: Sailor V", ReleaseDate = new DateTime(1991, 8, 3), Synopsis = "The story of Sailor Venus, a precursor to the Sailor Moon series.", Demographics = Demographics.Shoujo, TotalChapters = 15, IsCompleted = true, IsAiring = false, AuthorId = 6 },
            
            // Yoshihiro Togashi
            new() { Id = 11, Title = "Hunter x Hunter", ReleaseDate = new DateTime(1998, 3, 3), Synopsis = "A young boy searches for his hunter father.", Demographics = Demographics.Shounen, TotalChapters = 390, IsCompleted = false, IsAiring = true, AuthorId = 7 },
            new() { Id = 12, Title = "Yu Yu Hakusho", ReleaseDate = new DateTime(1990, 12, 3), Synopsis = "A teenage delinquent with spirit powers protects the human world.", Demographics = Demographics.Shounen, TotalChapters = 175, IsCompleted = true, IsAiring = false, AuthorId = 7 },
            
            // Kentaro Miura
            new() { Id = 13, Title = "Berserk", ReleaseDate = new DateTime(1989, 8, 25), Synopsis = "A lone swordsman battles evil forces in a dark medieval world.", Demographics = Demographics.Seinen, TotalChapters = 364, IsCompleted = false, IsAiring = true, AuthorId = 8 },
            
            // Rumiko Takahashi
            new() { Id = 14, Title = "InuYasha", ReleaseDate = new DateTime(1996, 11, 13), Synopsis = "A teenage girl travels to the past and joins a half-demon on adventures.", Demographics = Demographics.Shoujo, TotalChapters = 558, IsCompleted = true, IsAiring = false, AuthorId = 9 },
            new() { Id = 15, Title = "Ranma ½", ReleaseDate = new DateTime(1987, 8, 19), Synopsis = "A martial artist with a strange curse transforms into a girl when splashed with cold water.", Demographics = Demographics.Shoujo, TotalChapters = 407, IsCompleted = true, IsAiring = false, AuthorId = 9 },
            
            // Clamp
            new() { Id = 16, Title = "Cardcaptor Sakura", ReleaseDate = new DateTime(1996, 5, 1), Synopsis = "A girl discovers she has magical powers and must capture mystical cards.", Demographics = Demographics.Shoujo, TotalChapters = 50, IsCompleted = true, IsAiring = false, AuthorId = 10 },
            new() { Id = 17, Title = "XXXHolic", ReleaseDate = new DateTime(2003, 2, 24), Synopsis = "A young man is drawn into supernatural encounters at a mysterious shop.", Demographics = Demographics.Seinen, TotalChapters = 213, IsCompleted = true, IsAiring = false, AuthorId = 10 },
            new() { Id = 18, Title = "Tsubasa: Reservoir Chronicle", ReleaseDate = new DateTime(2003, 5, 21), Synopsis = "A cross-dimensional journey of characters from various Clamp works.", Demographics = Demographics.Shounen, TotalChapters = 233, IsCompleted = true, IsAiring = false, AuthorId = 10 },
            
            // Additional popular manga
            new() { Id = 19, Title = "Death Note", ReleaseDate = new DateTime(2003, 12, 1), Synopsis = "A high school student finds a notebook that allows him to kill anyone by writing their name.", Demographics = Demographics.Seinen, TotalChapters = 108, IsCompleted = true, IsAiring = false, AuthorId = 3 },
            new() { Id = 20, Title = "Tokyo Ghoul", ReleaseDate = new DateTime(2011, 9, 8), Synopsis = "A college student becomes a half-ghoul after an encounter with a monster.", Demographics = Demographics.Seinen, TotalChapters = 143, IsCompleted = true, IsAiring = false, AuthorId = 7 }
        };

        builder.Entity<Contracts.Models.Manga>().HasData(mangas);
    }
}