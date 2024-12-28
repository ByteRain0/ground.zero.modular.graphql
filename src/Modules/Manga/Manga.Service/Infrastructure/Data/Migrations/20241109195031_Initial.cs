using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Manga.Service.src.Manga.Service.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mangas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Synopsis = table.Column<string>(type: "text", nullable: false),
                    Demographics = table.Column<int>(type: "integer", nullable: false),
                    TotalChapters = table.Column<int>(type: "integer", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsAiring = table.Column<bool>(type: "boolean", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mangas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mangas_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Eiichiro Oda" },
                    { 2, "Masashi Kishimoto" },
                    { 3, "Tite Kubo" },
                    { 4, "Hajime Isayama" },
                    { 5, "Akira Toriyama" },
                    { 6, "Naoko Takeuchi" },
                    { 7, "Yoshihiro Togashi" },
                    { 8, "Kentaro Miura" },
                    { 9, "Rumiko Takahashi" },
                    { 10, "Clamp" }
                });

            migrationBuilder.InsertData(
                table: "Mangas",
                columns: new[] { "Id", "AuthorId", "Demographics", "IsAiring", "IsCompleted", "ReleaseDate", "Synopsis", "Title", "TotalChapters" },
                values: new object[,]
                {
                    { 1, 1, 0, true, false, new DateTime(1997, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "A young pirate searches for the ultimate treasure.", "One Piece", 1000 },
                    { 2, 2, 0, false, true, new DateTime(1999, 9, 21, 0, 0, 0, 0, DateTimeKind.Utc), "A ninja seeks recognition and dreams of becoming the Hokage.", "Naruto", 700 },
                    { 3, 2, 0, true, false, new DateTime(2016, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), "The next generation of ninjas, including Naruto's son, embarks on new adventures.", "Boruto: Naruto Next Generations", 63 },
                    { 4, 3, 0, false, true, new DateTime(2001, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc), "A teenager gains soul reaper powers to protect the living and dead.", "Bleach", 686 },
                    { 5, 3, 2, false, true, new DateTime(2018, 7, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Two witches protect and control dragons in a parallel world.", "Burn the Witch", 5 },
                    { 6, 4, 2, false, true, new DateTime(2009, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Humanity battles titans for survival within walls.", "Attack on Titan", 139 },
                    { 7, 5, 0, false, true, new DateTime(1984, 12, 3, 0, 0, 0, 0, DateTimeKind.Utc), "A young martial artist embarks on adventures to find dragon balls.", "Dragon Ball", 519 },
                    { 8, 5, 0, false, true, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "The adventures of a brilliant but quirky inventor and his robot girl.", "Dr. Slump", 236 },
                    { 9, 6, 1, false, true, new DateTime(1991, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), "A teenage girl transforms into a warrior to save the world.", "Sailor Moon", 60 },
                    { 10, 6, 1, false, true, new DateTime(1991, 8, 3, 0, 0, 0, 0, DateTimeKind.Utc), "The story of Sailor Venus, a precursor to the Sailor Moon series.", "Codename: Sailor V", 15 },
                    { 11, 7, 0, true, false, new DateTime(1998, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "A young boy searches for his hunter father.", "Hunter x Hunter", 390 },
                    { 12, 7, 0, false, true, new DateTime(1990, 12, 3, 0, 0, 0, 0, DateTimeKind.Utc), "A teenage delinquent with spirit powers protects the human world.", "Yu Yu Hakusho", 175 },
                    { 13, 8, 2, true, false, new DateTime(1989, 8, 25, 0, 0, 0, 0, DateTimeKind.Utc), "A lone swordsman battles evil forces in a dark medieval world.", "Berserk", 364 },
                    { 14, 9, 1, false, true, new DateTime(1996, 11, 13, 0, 0, 0, 0, DateTimeKind.Utc), "A teenage girl travels to the past and joins a half-demon on adventures.", "InuYasha", 558 },
                    { 15, 9, 1, false, true, new DateTime(1987, 8, 19, 0, 0, 0, 0, DateTimeKind.Utc), "A martial artist with a strange curse transforms into a girl when splashed with cold water.", "Ranma ½", 407 },
                    { 16, 10, 1, false, true, new DateTime(1996, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A girl discovers she has magical powers and must capture mystical cards.", "Cardcaptor Sakura", 50 },
                    { 17, 10, 2, false, true, new DateTime(2003, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "A young man is drawn into supernatural encounters at a mysterious shop.", "XXXHolic", 213 },
                    { 18, 10, 0, false, true, new DateTime(2003, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc), "A cross-dimensional journey of characters from various Clamp works.", "Tsubasa: Reservoir Chronicle", 233 },
                    { 19, 3, 2, false, true, new DateTime(2003, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A high school student finds a notebook that allows him to kill anyone by writing their name.", "Death Note", 108 },
                    { 20, 7, 2, false, true, new DateTime(2011, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "A college student becomes a half-ghoul after an encounter with a monster.", "Tokyo Ghoul", 143 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_AuthorId",
                table: "Mangas",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mangas");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
