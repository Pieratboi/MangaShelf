using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaShelf.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMangaDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Manga",
                type: "TEXT",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Manga",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "A dark fantasy manga following Guts, a lone swordsman marked by tragedy and revenge.");

            migrationBuilder.UpdateData(
                table: "Manga",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "A historical samurai manga inspired by the life of Miyamoto Musashi.");

            migrationBuilder.UpdateData(
                table: "Manga",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "A historical manga about war, revenge, slavery, and the search for a peaceful land.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Manga");
        }
    }
}
