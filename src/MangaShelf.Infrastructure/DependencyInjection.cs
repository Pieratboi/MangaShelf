using MangaShelf.Application.Manga;
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

        return services;
    }
}