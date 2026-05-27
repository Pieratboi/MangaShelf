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
        if(!Enum.TryParse<MangaStatus>(request.Status, ignoreCase: true, out var status) 
        || !Enum.IsDefined(status))
        {
            throw new ApplicationValidationException("Invalid Manga Status");
        }

        var manga = new Domain.Entities.Manga
        (
            title: request.Title,
            status: status,
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
            Status = createdManga.Status.ToString()
        };
    }
}