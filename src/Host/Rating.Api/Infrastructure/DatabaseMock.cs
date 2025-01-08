namespace Rating.Api.Infrastructure
{
    public class DatabaseMock
    {
        public DatabaseMock()
        {
            Ratings = new List<Domain.Rating>
            {
                // Manga Ratings
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 1, EntityType = "Manga", Mark = 4.8 }, // One Piece
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 2, EntityType = "Manga", Mark = 4.7 }, // Naruto
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 3, EntityType = "Manga", Mark = 4.5 }, // Boruto
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 4, EntityType = "Manga", Mark = 4.6 }, // Bleach
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 5, EntityType = "Manga", Mark = 4.3 }, // Burn the Witch
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 6, EntityType = "Manga", Mark = 4.9 }, // Attack on Titan
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 7, EntityType = "Manga", Mark = 4.8 }, // Dragon Ball
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 8, EntityType = "Manga", Mark = 4.4 }, // Dr. Slump
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 9, EntityType = "Manga", Mark = 4.7 }, // Sailor Moon
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 10, EntityType = "Manga", Mark = 4.5 }, // Codename: Sailor V
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 11, EntityType = "Manga", Mark = 4.8 }, // Hunter x Hunter
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 12, EntityType = "Manga", Mark = 4.6 }, // Yu Yu Hakusho
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 13, EntityType = "Manga", Mark = 4.9 }, // Berserk
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 14, EntityType = "Manga", Mark = 4.7 }, // InuYasha
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 15, EntityType = "Manga", Mark = 4.5 }, // Ranma ½
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 16, EntityType = "Manga", Mark = 4.6 }, // Cardcaptor Sakura
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 17, EntityType = "Manga", Mark = 4.3 }, // XXXHolic
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 18, EntityType = "Manga", Mark = 4.4 }, // Tsubasa: Reservoir Chronicle
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 19, EntityType = "Manga", Mark = 4.8 }, // Death Note
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 20, EntityType = "Manga", Mark = 4.7 }, // Tokyo Ghoul

                // Anime Ratings
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 1, EntityType = "Anime", Mark = 4.9 }, // My Neighbor Totoro
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 2, EntityType = "Anime", Mark = 5.0 }, // Spirited Away
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 3, EntityType = "Anime", Mark = 4.7 }, // Dragon Ball Z
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 4, EntityType = "Anime", Mark = 4.8 }, // One Piece
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 5, EntityType = "Anime", Mark = 4.9 }, // Death Note
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 6, EntityType = "Anime", Mark = 4.8 }, // Hunter x Hunter
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 7, EntityType = "Anime", Mark = 4.8 }, // Cowboy Bebop
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 8, EntityType = "Anime", Mark = 4.6 }, // Mobile Suit Gundam
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 9, EntityType = "Anime", Mark = 4.9 }, // Fullmetal Alchemist: Brotherhood
                new Domain.Rating { Id = Guid.NewGuid(), EntityId = 10, EntityType = "Anime", Mark = 4.7 }  // My Hero Academia
            };
        }

        public List<Domain.Rating> Ratings { get; set; }
    }
}
