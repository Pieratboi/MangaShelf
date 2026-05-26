using MangaShelf.Application.Common.Exceptions;
using MangaShelf.Domain.Enums;

namespace MangaShelf.Application.Manga.UpdateStatus;

public class UpdateMangaStatusUseCase
{
    private readonly IMangaRepository _mangaRepository;

    public UpdateMangaStatusUseCase(IMangaRepository mangaRepository)
    {
        _mangaRepository = mangaRepository;
    }

    public async Task<UpdateMangaStatusResponse?> ExecuteAsync(int id, UpdateMangaStatusRequest request)
    {
        if(id <= 0)
        {
            throw new ApplicationValidationException("Manga id must be greater than 0.");
        }

        if(!Enum.TryParse<MangaStatus>(request.Status, ignoreCase: true, out var status) 
        || !Enum.IsDefined(status))
        {
            throw new ApplicationValidationException("Invalid Manga status.");
        }

        var manga = await _mangaRepository.GetByIdForUpdateAsync(id);

        if(manga is null)
        {
            return null;
        }

        manga.ChangeStatus(status);

        await _mangaRepository.SaveChangesAsync();

        return new UpdateMangaStatusResponse
        {
          Id = manga.Id,
          Title = manga.Title,
          Description = manga.Description,
          Status = manga.Status.ToString()  
        };
    }
}