using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaShelf.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedChapterPages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChapterPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChapterReleaseId = table.Column<int>(type: "INTEGER", nullable: false),
                    PageNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterPages_ChapterReleases_ChapterReleaseId",
                        column: x => x.ChapterReleaseId,
                        principalTable: "ChapterReleases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChapterPages_ChapterReleaseId_PageNumber",
                table: "ChapterPages",
                columns: new[] { "ChapterReleaseId", "PageNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChapterPages");
        }
    }
}
