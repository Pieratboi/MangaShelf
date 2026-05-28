using MangaShelf.Application.Common.Exceptions;

namespace MangaShelf.Application.Manga.UpdateDetails;

public class UpdateMangaDetailsUseCase
{
    private readonly IMangaRepository _mangaRepository;

    public UpdateMangaDetailsUseCase(IMangaRepository mangaRepository)
    {
        _mangaRepository = mangaRepository;
    }

    public async Task<UpdateMangaDetailsResponse?> ExecuteAsync(
        int id, UpdateMangaDetailsRequest request)
    {
        if(id <= 0)
        {
            throw new ApplicationValidationException("Manga id must be greater than 0.");
        }

        if(request.Title is null 
            && request.Author is null 
            && request.Artist is null 
            && request.Description is null)
        {
            throw new ApplicationValidationException("At least one manga detail must be provided.");
        }

        var manga = await _mangaRepository.GetByIdForUpdateAsync(id);

        if(manga is null)
        {
            return null;
        }

        if(request.Title is not null)
        {
            manga.ChangeTitle(request.Title);
        }

        if(request.Author is not null)
        {
            manga.ChangeAuthor(request.Author);
        }

        if(request.Artist is not null)
        {
            manga.ChangeArtist(request.Artist);
        }

        if(request.Description is not null)
        {
            manga.ChangeDescription(request.Description);
        }

        await _mangaRepository.SaveChangesAsync();

        return new UpdateMangaDetailsResponse
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