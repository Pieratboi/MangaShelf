using MangaShelf.Application.Manga;

namespace MangaShelf.Application.Manga.GetById;

public class GetMangaByIdUseCase
{
    private readonly IMangaRepository _mangaRepository;

    public GetMangaByIdUseCase(IMangaRepository mangaRepository)
    {
        _mangaRepository = mangaRepository;
    }

    public async Task<MangaDetailsResponse?> ExecuteAsync(int id)
    {
        var manga = await _mangaRepository.GetByIdAsync(id);

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