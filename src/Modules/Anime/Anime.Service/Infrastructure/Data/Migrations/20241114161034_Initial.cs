using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace anime.Service.src.Anime.Service.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Studios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Animes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    StudioId = table.Column<int>(type: "integer", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Synopsis = table.Column<string>(type: "text", nullable: false),
                    Demographics = table.Column<string>(type: "text", nullable: false, defaultValue: "Shounen"),
                    TotalEpisodes = table.Column<int>(type: "integer", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsAiring = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animes_Studios_StudioId",
                        column: x => x.StudioId,
                        principalTable: "Studios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Studios",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Studio Ghibli" },
                    { 2, "Toei Animation" },
                    { 3, "Madhouse" },
                    { 4, "Sunrise" },
                    { 5, "Bones" }
                });

            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "Id", "Demographics", "IsCompleted", "ReleaseDate", "StudioId", "Synopsis", "Title", "TotalEpisodes" },
                values: new object[,]
                {
                    { 1, "Shoujo", true, new DateTime(1988, 4, 16, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Two girls move to the countryside and encounter magical creatures.", "My Neighbor Totoro", 1 },
                    { 2, "Shoujo", true, new DateTime(2001, 7, 20, 0, 0, 0, 0, DateTimeKind.Utc), 1, "A young girl finds herself in a mysterious spirit world.", "Spirited Away", 1 }
                });

            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "Id", "IsCompleted", "ReleaseDate", "StudioId", "Synopsis", "Title", "TotalEpisodes" },
                values: new object[] { 3, true, new DateTime(1989, 4, 26, 0, 0, 0, 0, DateTimeKind.Utc), 2, "The adventures of Goku as he defends Earth from powerful enemies.", "Dragon Ball Z", 291 });

            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "Id", "IsAiring", "ReleaseDate", "StudioId", "Synopsis", "Title", "TotalEpisodes" },
                values: new object[] { 4, true, new DateTime(1999, 10, 20, 0, 0, 0, 0, DateTimeKind.Utc), 2, "A young pirate searches for the ultimate treasure.", "One Piece", 1000 });

            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "Id", "Demographics", "IsCompleted", "ReleaseDate", "StudioId", "Synopsis", "Title", "TotalEpisodes" },
                values: new object[] { 5, "Seinen", true, new DateTime(2006, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), 3, "A high school student finds a notebook that allows him to kill anyone by writing their name.", "Death Note", 37 });

            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "Id", "IsCompleted", "ReleaseDate", "StudioId", "Synopsis", "Title", "TotalEpisodes" },
                values: new object[] { 6, true, new DateTime(2011, 10, 2, 0, 0, 0, 0, DateTimeKind.Utc), 3, "A young boy searches for his hunter father.", "Hunter x Hunter", 148 });

            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "Id", "Demographics", "IsCompleted", "ReleaseDate", "StudioId", "Synopsis", "Title", "TotalEpisodes" },
                values: new object[] { 7, "Seinen", true, new DateTime(1998, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), 4, "A crew of bounty hunters chase criminals across the galaxy.", "Cowboy Bebop", 26 });

            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "Id", "IsCompleted", "ReleaseDate", "StudioId", "Synopsis", "Title", "TotalEpisodes" },
                values: new object[,]
                {
                    { 8, true, new DateTime(1979, 4, 7, 0, 0, 0, 0, DateTimeKind.Utc), 4, "A young pilot fights in a futuristic war with a giant robot suit.", "Mobile Suit Gundam", 43 },
                    { 9, true, new DateTime(2009, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc), 5, "Two brothers use alchemy to try to restore their bodies after a failed experiment.", "Fullmetal Alchemist: Brotherhood", 64 }
                });

            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "Id", "IsAiring", "ReleaseDate", "StudioId", "Synopsis", "Title", "TotalEpisodes" },
                values: new object[] { 10, true, new DateTime(2016, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), 5, "In a world where nearly everyone has superpowers, a powerless boy trains to be a hero.", "My Hero Academia", 113 });

            migrationBuilder.CreateIndex(
                name: "IX_Animes_StudioId",
                table: "Animes",
                column: "StudioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animes");

            migrationBuilder.DropTable(
                name: "Studios");
        }
    }
}
