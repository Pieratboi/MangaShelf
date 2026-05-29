using MangaShelf.Application.Manga.Create;
using MangaShelf.Application.Manga.Delete;
using MangaShelf.Application.Manga.GetAll;
using MangaShelf.Application.Manga.GetById;
using MangaShelf.Application.Manga.UpdatePublicationStatus;
using MangaShelf.Application.Manga.UpdateDetails;
using MangaShelf.Application.Chapters.Create;
using MangaShelf.Application.Chapters.GetByMangaId;
using MangaShelf.Application.Chapters.GetById;
using MangaShelf.Application.Chapters.Update;
using MangaShelf.Application.Chapters.Delete;
using MangaShelf.Application.Scanlators.Create;
using MangaShelf.Application.Scanlators.GetAll;
using MangaShelf.Application.ChapterReleases.Create;
using MangaShelf.Application.ChapterReleases.GetByChapter;
using MangaShelf.Application.ChapterReleases.GetById;
using MangaShelf.Application.ChapterReleases.Update;
using MangaShelf.Application.ChapterReleases.Delete;
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
        
        services.AddScoped<CreateChapterUseCase>();
        services.AddScoped<GetChaptersByMangaIdUseCase>();
        services.AddScoped<GetChapterByIdUseCase>();
        services.AddScoped<UpdateChapterUseCase>();
        services.AddScoped<DeleteChapterUseCase>();

        services.AddScoped<CreateScanlatorUseCase>();
        services.AddScoped<GetAllScanlatorsUseCase>();

        services.AddScoped<CreateChapterReleaseUseCase>();
        services.AddScoped<GetChapterReleasesUseCase>();
        services.AddScoped<GetChapterReleaseByIdUseCase>();
        services.AddScoped<UpdateChapterReleaseUseCase>();
        services.AddScoped<DeleteChapterReleaseUseCase>();

        return services;
    }
}