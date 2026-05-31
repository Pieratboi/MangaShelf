using MangaShelf.Application.Manga;
using MangaShelf.Application.Chapters;
using MangaShelf.Application.Scanlators;
using MangaShelf.Application.ChapterReleases;
using MangaShelf.Application.ChapterPages;
using MangaShelf.Infrastructure.Persistence;
using MangaShelf.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MangaShelf.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddDbContext<MangaShelfDbContext>(options =>
        {
           options.UseSqlite(
            configuration.GetConnectionString("DefaultConnection")
           ); 
        });

        services.AddScoped<IMangaRepository, EfCoreMangaRepository>();
        services.AddScoped<IChapterRepository, EfCoreChapterRepository>();
        services.AddScoped<IScanlatorRepository, EfCoreScanlatorRepository>();
        services.AddScoped<IChapterReleaseRepository, EfCoreChapterReleaseRepository>();
        services.AddScoped<IChapterPageRepository, EfCoreChapterPageRepository>();

        return services;
    }
}