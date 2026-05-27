using System.Data.Common;
using System.Reflection;
using MangaShelf.Application.Manga;

namespace MangaShelf.Application.Manga.GetAll;

public class GetAllMangaUseCase
{
    private readonly IMangaRepository _mangaRepository;

    public GetAllMangaUseCase(IMangaRepository mangaRepository)
    {
        _mangaRepository = mangaRepository;
    }

    public async Task<List<MangaResponse>> ExecuteAsync()
    {
        var manga = await _mangaRepository.GetAllAsync();

        return manga.Select(m => new MangaResponse
        {
            Id = m.Id,
            Title = m.Title,
            Author = m.Author,
            Artist = m.Artist,
            PublicationStatus = m.PublicationStatus.ToString()
        }).ToList();
    }
}