using MangaShelf.Application.Manga.Create;
using MangaShelf.Application.Manga.Delete;
using MangaShelf.Application.Manga.GetAll;
using MangaShelf.Application.Manga.GetById;
using MangaShelf.Application.Manga.UpdatePublicationStatus;
using MangaShelf.Application.Manga.UpdateDetails;
using Microsoft.Extensions.DependencyInjection;

namespace MangaShelf.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateMangaUseCase>();
        services.AddScoped<DeleteMangaUseCase>();
        services.AddScoped<GetAllMangaUseCase>();
        services.AddScoped<GetMangaByIdUseCase>();
        services.AddScoped<UpdateMangaPublicationStatusUseCase>();
        services.AddScoped<UpdateMangaDetailsUseCase>();

        return services;
    }
}