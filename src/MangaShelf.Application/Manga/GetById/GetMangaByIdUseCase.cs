using MangaShelf.Application.Manga;

namespace MangaShelf.Application.Manga.GetById;

public class GetMangaByIdUseCase
{
    private readonly IMangaRepository _mangaRepository;

    public GetMangaByIdUseCase(IMangaRepository mangaRepository)
    {
        _mangaRepository = mangaRepository;
    }

    public MangaDetailsResponse? Execute(int id)
    {
        var manga = _mangaRepository.GetById(id);

        if(manga is null)
        {
            return null;
        }

        return new MangaDetailsResponse
        {
            Id = manga.Id,
            Title = manga.Title,
            Status = manga.Status.ToString()
        };
    }
}