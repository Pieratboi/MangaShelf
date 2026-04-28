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

    public List<MangaResponse> Execute()
    {
        var manga = _mangaRepository.GetAll();

        return manga.Select(m => new MangaResponse
        {
            Id = m.Id,
            Title = m.Title,
            Status = m.Status.ToString()
        }).ToList();
    }
}