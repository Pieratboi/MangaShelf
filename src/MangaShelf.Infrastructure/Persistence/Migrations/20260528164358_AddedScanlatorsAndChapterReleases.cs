using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaShelf.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedScanlatorsAndChapterReleases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scanlators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    WebsiteUrl = table.Column<string>(type: "TEXT", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scanlators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChapterReleases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChapterId = table.Column<int>(type: "INTEGER", nullable: false),
                    ScanlatorId = table.Column<int>(type: "INTEGER", nullable: false),
                    SourceUrl = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Language = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterReleases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterReleases_Chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChapterReleases_Scanlators_ScanlatorId",
                        column: x => x.ScanlatorId,
                        principalTable: "Scanlators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChapterReleases_ChapterId_ScanlatorId",
                table: "ChapterReleases",
                columns: new[] { "ChapterId", "ScanlatorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChapterReleases_ScanlatorId",
                table: "ChapterReleases",
                column: "ScanlatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Scanlators_Name",
                table: "Scanlators",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChapterReleases");

            migrationBuilder.DropTable(
                name: "Scanlators");
        }
    }
}
