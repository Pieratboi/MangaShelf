using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaShelf.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredMangaStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Manga",
                newName: "PublicationStatus");

            migrationBuilder.Sql("""
                UPDATE Manga
                SET PublicationStatus =
                    CASE PublicationStatus
                        WHEN 'Reading' THEN 'Publishing'
                        WHEN 'PlanToRead' THEN 'Publishing'
                        WHEN 'Completed' THEN 'Completed'
                        WHEN 'Dropped' THEN 'Discontinued'
                        WHEN 'OnHold' Then 'Hiatus'
                        ELSE 'Unknown'
                    END
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE Manga
                SET PublicationStatus =
                    CASE PublicationStatus
                        WHEN 'Publishing' THEN 'Reading'
                        WHEN 'Completed' THEN 'Completed'
                        WHEN 'Hiatus' THEN 'OnHold'
                        WHEN 'Discontinued' THEN 'Dropped'
                        WHEN 'Unknown' THEN 'PlanToRead'
                        ELSE 'PlanToRead'
                    END
            """);

            migrationBuilder.RenameColumn(
                name: "PublicationStatus",
                table: "Manga",
                newName: "Status");
        }
    }
}
