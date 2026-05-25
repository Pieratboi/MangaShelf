using MangaShelf.Application.Common.Exceptions;

namespace MangaShelf.Application.Manga.Delete;

public class DeleteMangaUseCase
{
    private readonly IMangaRepository _mangaRepository;

    public DeleteMangaUseCase(IMangaRepository mangaRepository)
    {
        _mangaRepository = mangaRepository;
    }

    public async Task<bool> ExecuteAsync(int id)
    {
        if(id <= 0)
        {
            throw new ApplicationValidationException("Manga id must be greater than 0.");
        }

        var manga = await _mangaRepository.GetByIdForUpdateAsync(id);

        if(manga is null)
        {
            return false;
        }

        _mangaRepository.Delete(manga);

        await _mangaRepository.SaveChangesAsync();
        
        return true;
    }
}