using MangaShelf.Application.Common.Exceptions;
using MangaShelf.Domain.Enums;

namespace MangaShelf.Application.Manga.UpdatePublicationStatus;

public class UpdateMangaPublicationStatusUseCase
{
    private readonly IMangaRepository _mangaRepository;

    public UpdateMangaPublicationStatusUseCase(IMangaRepository mangaRepository)
    {
        _mangaRepository = mangaRepository;
    }

    public async Task<UpdateMangaPublicationStatusResponse?> ExecuteAsync(int id, UpdateMangaPublicationStatusRequest request)
    {
        if(id <= 0)
        {
            throw new ApplicationValidationException("Manga id must be greater than 0.");
        }

        if(!Enum.TryParse<PublicationStatus>(request.PublicationStatus, ignoreCase: true, out var publicationStatus) 
        || !Enum.IsDefined(publicationStatus))
        {
            throw new ApplicationValidationException("Invalid manga publication status.");
        }

        var manga = await _mangaRepository.GetByIdForUpdateAsync(id);

        if(manga is null)
        {
            return null;
        }

        manga.ChangePublicationStatus(publicationStatus);

        await _mangaRepository.SaveChangesAsync();

        return new UpdateMangaPublicationStatusResponse
        {
          Id = manga.Id,
          Title = manga.Title,
          Author = manga.Author,
          Artist = manga.Artist,
          Description = manga.Description,
          PublicationStatus = manga.PublicationStatus.ToString()  
        };
    }
}