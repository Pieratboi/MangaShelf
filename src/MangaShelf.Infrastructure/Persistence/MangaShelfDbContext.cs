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

            entity.Property(m => m.Author)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(m => m.Artist)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(2000);

            entity.Property(m => m.PublicationStatus)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(50);
            
            entity.HasData(
                new
                {
                    Id = 1,
                    Title = "Berserk",
                    Author = "Kentaro Miura",
                    Artist = "Kentaro Miura",
                    Description = "A dark fantasy manga following Guts, a lone swordsman marked by tragedy and revenge.",
                    PublicationStatus = PublicationStatus.Hiatus
                },
                new
                {
                    Id = 2,
                    Title = "Vagabond",
                    Author = "Takehiko Inoue",
                    Artist = "Takehiko Inoue",
                    Description = "A historical samurai manga inspired by the life of Miyamoto Musashi.",
                    PublicationStatus = PublicationStatus.Discontinued
                },
                new
                {
                    Id = 3,
                    Title = "Vinland Saga",
                    Author = "Makoto Yukimura",
                    Artist = "Makoto Yukimura",
                    Description = "A historical manga about war, revenge, slavery, and the search for a peaceful land.",
                    PublicationStatus = PublicationStatus.Completed
                }
            );
        });
    }
}