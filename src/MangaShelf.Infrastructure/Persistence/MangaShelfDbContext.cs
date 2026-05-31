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
    public DbSet<Chapter> Chapters => Set<Chapter>();
    public DbSet<Scanlator> Scanlators => Set<Scanlator>();
    public DbSet<ChapterRelease> ChapterReleases => Set<ChapterRelease>();
    public DbSet<ChapterPage> ChapterPages => Set<ChapterPage>();

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

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.ToTable("Chapters");

            entity.HasKey(c => c.Id);

            entity.Property(c => c.Id)
                .ValueGeneratedOnAdd();
            
            entity.Property(c => c.MangaId)
                .IsRequired();
            
            entity.Property(c => c.Number)
                .IsRequired();

            entity.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(c => new {c.MangaId, c.Number})
                .IsUnique();

            entity.HasOne(c => c.Manga)
                .WithMany()
                .HasForeignKey(c => c.MangaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Scanlator>(entity =>
        {
            entity.ToTable("Scanlators");

            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .ValueGeneratedOnAdd();
            
            entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(s => s.WebsiteUrl)
                .IsRequired()
                .HasMaxLength(400);
            
            entity.HasIndex(s => s.Name)
                .IsUnique();
        });

        modelBuilder.Entity<ChapterRelease>(entity =>
        {
            entity.ToTable("ChapterReleases");

            entity.HasKey(r => r.Id);

            entity.Property(r => r.Id)
                .ValueGeneratedOnAdd();
            
            entity.Property(r => r.ChapterId)
                .IsRequired();
            
            entity.Property(r => r.ScanlatorId)
                .IsRequired();
            
            entity.Property(r => r.SourceUrl)
                .IsRequired()
                .HasMaxLength(1000);
            
            entity.Property(r => r.Language)
                .IsRequired()
                .HasMaxLength(20);
            
            entity.HasIndex(r => new {r.ChapterId, r.ScanlatorId})
                .IsUnique();
            
            entity.HasOne(r => r.Chapter)
                .WithMany()
                .HasForeignKey(r => r.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(r => r.Scanlator)
                .WithMany()
                .HasForeignKey(r => r.ScanlatorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ChapterPage>(entity =>
        {
            entity.ToTable("ChapterPages");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            entity.Property(p => p.ChapterReleaseId)
                .IsRequired();
            
            entity.Property(p => p.PageNumber)
                .IsRequired();
            
            entity.Property(p => p.ImageUrl)
                .IsRequired()
                .HasMaxLength(1000);
            
            entity.HasIndex(p => new {p.ChapterReleaseId, p.PageNumber})
                .IsUnique();
            
            entity.HasOne(p => p.ChapterRelease)
                .WithMany()
                .HasForeignKey(p => p.ChapterReleaseId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}