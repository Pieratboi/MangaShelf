using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaShelf.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMangaCred : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Artist",
                table: "Manga",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Manga",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Manga",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Artist", "Author" },
                values: new object[] { "Kentaro Miura", "Kentaro Miura" });

            migrationBuilder.UpdateData(
                table: "Manga",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Artist", "Author" },
                values: new object[] { "Takehiko Inoue", "Takehiko Inoue" });

            migrationBuilder.UpdateData(
                table: "Manga",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Artist", "Author" },
                values: new object[] { "Makoto Yukimura", "Makoto Yukimura" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Artist",
                table: "Manga");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Manga");
        }
    }
}
