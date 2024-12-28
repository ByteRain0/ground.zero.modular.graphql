﻿// <auto-generated />
using System;
using Manga.Service.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Manga.Service.src.Manga.Service.Infrastructure.Data.Migrations
{
    [DbContext(typeof(MangaDbContext))]
    partial class MangaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Manga.Contracts.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Eiichiro Oda"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Masashi Kishimoto"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Tite Kubo"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Hajime Isayama"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Akira Toriyama"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Naoko Takeuchi"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Yoshihiro Togashi"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Kentaro Miura"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Rumiko Takahashi"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Clamp"
                        });
                });

            modelBuilder.Entity("Manga.Contracts.Models.Manga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<int>("Demographics")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAiring")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Synopsis")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalChapters")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Mangas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Demographics = 0,
                            IsAiring = true,
                            IsCompleted = false,
                            ReleaseDate = new DateTime(1997, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "A young pirate searches for the ultimate treasure.",
                            Title = "One Piece",
                            TotalChapters = 1000
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 2,
                            Demographics = 0,
                            IsAiring = false,
                            IsCompleted = true,
                            ReleaseDate = new DateTime(1999, 9, 21, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "A ninja seeks recognition and dreams of becoming the Hokage.",
                            Title = "Naruto",
                            TotalChapters = 700
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 2,
                            Demographics = 0,
                            IsAiring = true,
                            IsCompleted = false,
                            ReleaseDate = new DateTime(2016, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "The next generation of ninjas, including Naruto's son, embarks on new adventures.",
                            Title = "Boruto: Naruto Next Generations",
                            TotalChapters = 63
                        },
                        new
                        {
                            Id = 4,
                            AuthorId = 3,
                            Demographics = 0,
                            IsAiring = false,
                            IsCompleted = true,
                            ReleaseDate = new DateTime(2001, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "A teenager gains soul reaper powers to protect the living and dead.",
                            Title = "Bleach",
                            TotalChapters = 686
                        },
                        new
                        {
                            Id = 5,
                            AuthorId = 3,
                            Demographics = 2,
                            IsAiring = false,
                            IsCompleted = true,
                            ReleaseDate = new DateTime(2018, 7, 16, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "Two witches protect and control dragons in a parallel world.",
                            Title = "Burn the Witch",
                            TotalChapters = 5
                        },
                        new
                        {
                            Id = 6,
                            AuthorId = 4,
                            Demographics = 2,
                            IsAiring = false,
                            IsCompleted = true,
                            ReleaseDate = new DateTime(2009, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "Humanity battles titans for survival within walls.",
                            Title = "Attack on Titan",
                            TotalChapters = 139
                        },
                        new
                        {
                            Id = 7,
                            AuthorId = 5,
                            Demographics = 0,
                            IsAiring = false,
                            IsCompleted = true,
                            ReleaseDate = new DateTime(1984, 12, 3, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "A young martial artist embarks on adventures to find dragon balls.",
                            Title = "Dragon Ball",
                            TotalChapters = 519
                        },
                        new
                        {
                            Id = 8,
                            AuthorId = 5,
                            Demographics = 0,
                            IsAiring = false,
                            IsCompleted = true,
                            ReleaseDate = new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "The adventures of a brilliant but quirky inventor and his robot girl.",
                            Title = "Dr. Slump",
                            TotalChapters = 236
                        },
                        new
                        {
                            Id = 9,
                            AuthorId = 6,
                            Demographics = 1,
                            IsAiring = false,
                            IsCompleted = true,
                            ReleaseDate = new DateTime(1991, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "A teenage girl transforms into a warrior to save the world.",
                            Title = "Sailor Moon",
                            TotalChapters = 60
                        },
                        new
                        {
                            Id = 10,
                            AuthorId = 6,
                            Demographics = 1,
                            IsAiring = false,
                            IsCompleted = true,
                            ReleaseDate = new DateTime(1991, 8, 3, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "The story of Sailor Venus, a precursor to the Sailor Moon series.",
                            Title = "Codename: Sailor V",
                            TotalChapters = 15
                        },
                        new
                        {
                            Id = 11,
                            AuthorId = 7,
                            Demographics = 0,
                            IsAiring = true,
                            IsCompleted = false,
                            ReleaseDate = new DateTime(1998, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "A young boy searches for his hunter father.",
                            Title = "Hunter x Hunter",
                            TotalChapters = 390
                        },
                        new
                        {
                            Id = 12,
                            AuthorId = 7,
                            Demographics = 0,
                            IsAiring = false,
                            IsCompleted = true,
                            ReleaseDate = new DateTime(1990, 12, 3, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "A teenage delinquent with spirit powers protects the human world.",
                            Title = "Yu Yu Hakusho",
                            TotalChapters = 175
                        },
                        new
                        {
                            Id = 13,
                            AuthorId = 8,
                            Demographics = 2,
                            IsAiring = true,
                            IsCompleted = false,
                            ReleaseDate = new DateTime(1989, 8, 25, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "A lone swordsman battles evil forces in a dark medieval world.",
                            Title = "Berserk",
                            TotalChapters = 364
                        },
                        new
                        {
                            Id = 14,
                            AuthorId = 9,
                            Demographics = 1,
                            IsAiring = false,
                            IsCompleted = true,
                            ReleaseDate = new DateTime(1996, 11, 13, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "A teenage girl travels to the past and joins a half-demon on adventures.",
                            Title = "InuYasha",
                            TotalChapters = 558
                        },
                        new
                        {
                            Id = 15,
                            AuthorId = 9,
                            Demographics = 1,
                            IsAiring = false,
                            IsCompleted = true,
                            ReleaseDate = new DateTime(1987, 8, 19, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "A martial artist with a strange curse transforms into a girl when splashed with cold water.",
                            Title = "Ranma ½",
                            TotalChapters = 407
                        },
                        new
                        {
                            Id = 16,
                            AuthorId = 10,
                            Demographics = 1,
                            IsAiring = false,
                            IsCompleted = true,
                            ReleaseDate = new DateTime(1996, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "A girl discovers she has magical powers and must capture mystical cards.",
                            Title = "Cardcaptor Sakura",
                            TotalChapters = 50
                        },
                        new
                        {
                            Id = 17,
                            AuthorId = 10,
                            Demographics = 2,
                            IsAiring = false,
                            IsCompleted = true,
                            ReleaseDate = new DateTime(2003, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "A young man is drawn into supernatural encounters at a mysterious shop.",
                            Title = "XXXHolic",
                            TotalChapters = 213
                        },
                        new
                        {
                            Id = 18,
                            AuthorId = 10,
                            Demographics = 0,
                            IsAiring = false,
                            IsCompleted = true,
                            ReleaseDate = new DateTime(2003, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "A cross-dimensional journey of characters from various Clamp works.",
                            Title = "Tsubasa: Reservoir Chronicle",
                            TotalChapters = 233
                        },
                        new
                        {
                            Id = 19,
                            AuthorId = 3,
                            Demographics = 2,
                            IsAiring = false,
                            IsCompleted = true,
                            ReleaseDate = new DateTime(2003, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "A high school student finds a notebook that allows him to kill anyone by writing their name.",
                            Title = "Death Note",
                            TotalChapters = 108
                        },
                        new
                        {
                            Id = 20,
                            AuthorId = 7,
                            Demographics = 2,
                            IsAiring = false,
                            IsCompleted = true,
                            ReleaseDate = new DateTime(2011, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc),
                            Synopsis = "A college student becomes a half-ghoul after an encounter with a monster.",
                            Title = "Tokyo Ghoul",
                            TotalChapters = 143
                        });
                });

            modelBuilder.Entity("Manga.Contracts.Models.Manga", b =>
                {
                    b.HasOne("Manga.Contracts.Models.Author", "Author")
                        .WithMany("Mangas")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Manga.Contracts.Models.Author", b =>
                {
                    b.Navigation("Mangas");
                });
#pragma warning restore 612, 618
        }
    }
}
