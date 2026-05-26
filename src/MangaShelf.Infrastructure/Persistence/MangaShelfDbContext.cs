using MangaShelf.Domain.Entities;
using MangaShelf.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace MangaShelf.Infrastructure.Persistence;

public class MangaShelfDbContext : DbContext
{
    public MangaShelfDbContext(DbContextOptions<MangaShelfDbContext> options)
        : base(options)
    {
    }

    public DbSet<Manga> Manga => Set<Manga>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Manga>(entity =>
        {
            entity.ToTable("Manga");

            entity.HasKey(m => m.Id);

            entity.Property(m => m.Id)
                .ValueGeneratedOnAdd();
            
            entity.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(2000);

            entity.Property(m => m.Status)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(50);
            
            entity.HasData(
                new
                {
                    Id = 1,
                    Title = "Berserk",
                    Description = "A dark fantasy manga following Guts, a lone swordsman marked by tragedy and revenge.",
                    Status = MangaStatus.Reading
                },
                new
                {
                    Id = 2,
                    Title = "Vagabond",
                    Description = "A historical samurai manga inspired by the life of Miyamoto Musashi.",
                    Status = MangaStatus.PlanToRead
                },
                new
                {
                    Id = 3,
                    Title = "Vinland Saga",
                    Description = "A historical manga about war, revenge, slavery, and the search for a peaceful land.",
                    Status = MangaStatus.Completed
                }
            );
        });
    }
}