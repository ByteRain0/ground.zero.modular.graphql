using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace anime.Service.src.Anime.Service.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class DisableAnimteTitleCaseSensitivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:text_insensitive", "en-u-ks-primary,en-u-ks-primary,icu,False");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Animes",
                type: "text",
                nullable: false,
                collation: "text_insensitive",
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:CollationDefinition:text_insensitive", "en-u-ks-primary,en-u-ks-primary,icu,False");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Animes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "text_insensitive");
        }
    }
}
