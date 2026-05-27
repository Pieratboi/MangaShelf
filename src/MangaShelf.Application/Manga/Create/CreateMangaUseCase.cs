using MangaShelf.Domain.Entities;
using MangaShelf.Domain.Enums;
using MangaShelf.Application.Common.Exceptions;

namespace MangaShelf.Application.Manga.Create;

public class CreateMangaUseCase
{
    private readonly IMangaRepository _mangaRepository;

    public CreateMangaUseCase(IMangaRepository mangaRepository)
    {
        _mangaRepository = mangaRepository;
    }

    public async Task<CreateMangaResponse> ExecuteAsync(CreateMangaRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ApplicationValidationException("Title is required.");
        }
        if(!Enum.TryParse<PublicationStatus>(request.PublicationStatus, ignoreCase: true, out var publicationStatus) 
        || !Enum.IsDefined(publicationStatus))
        {
            throw new ApplicationValidationException("Invalid manga publication status.");
        }

        var manga = new Domain.Entities.Manga
        (
            title: request.Title,
            publicationStatus: publicationStatus,
            author: request.Author,
            artist: request.Artist,
            description: request.Description
        );

        var createdManga = await _mangaRepository.CreateAsync(manga);

        return new CreateMangaResponse
        {
            Id = createdManga.Id,
            Title = createdManga.Title,
            Author = createdManga.Author,
            Artist = createdManga.Artist,
            Description = createdManga.Description,
            PublicationStatus = createdManga.PublicationStatus.ToString()
        };
    }
}