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

            entity.Property(m => m.Status)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(50);
            
            entity.HasData(
                new
                {
                    Id = 1,
                    Title = "Berserk",
                    Status = MangaStatus.Reading
                },
                new
                {
                    Id = 2,
                    Title = "Vagabond",
                    Status = MangaStatus.PlanToRead
                },
                new
                {
                    Id = 3,
                    Title = "Vinland Saga",
                    Status = MangaStatus.Completed
                }
            );
        });
    }
}