using MangaShelf.Domain.Entities;
using MangaShelf.Domain.Enums;

namespace MangaShelf.Application.Manga.Create;

public class CreateMangaUseCase
{
    private readonly IMangaRepository _mangaRepository;

    public CreateMangaUseCase(IMangaRepository mangaRepository)
    {
        _mangaRepository = mangaRepository;
    }

    public CreateMangaResponse Execute(CreateMangaRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ArgumentException("Title is required.");
        }
        if(!Enum.TryParse<MangaStatus>(request.Status, ignoreCase: true, out var status))
        {
            throw new ArgumentException("Invalid Manga Status");
        }

        var existingManga = _mangaRepository.GetAll();

        var nextId = existingManga.Count == 0
            ? 1
            : existingManga.Max(m => m.Id) + 1;

        var manga = new Domain.Entities.Manga
        (
            nextId,
            request.Title,
            status
        );

        var createdManga = _mangaRepository.Create(manga);

        return new CreateMangaResponse
        {
            Id = createdManga.Id,
            Title = createdManga.Title,
            Status = createdManga.Status.ToString()
        };
    }
}