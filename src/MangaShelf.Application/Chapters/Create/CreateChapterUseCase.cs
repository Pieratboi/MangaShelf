using MangaShelf.Application.Common.Exceptions;
using MangaShelf.Application.Manga;
using MangaShelf.Domain.Entities;

namespace MangaShelf.Application.Chapters.Create;

public class CreateChapterUseCase
{
    private readonly IMangaRepository _mangaRepository;
    private readonly IChapterRepository _chapterRepository;

    public CreateChapterUseCase(IMangaRepository mangaRepository, IChapterRepository chapterRepository)
    {
        _mangaRepository = mangaRepository;
        _chapterRepository = chapterRepository;
    }

    public async Task<CreateChapterResponse?> ExecuteAsync(
        int mangaId, 
        CreateChapterRequest request)
    {
        if(mangaId <= 0)
        {
            throw new ApplicationValidationException("Manga id must be greater than 0.");
        }
        if(request.Number <= 0)
        {
            throw new ApplicationValidationException("Chapter number must be greater than 0.");
        }

        var manga = await _mangaRepository.GetByIdAsync(mangaId);

        if(manga is null)
        {
            return null;
        }

        var chapterAlreadyExists = await _chapterRepository
            .ExistsByMangaIdAndNumberAsync(mangaId, request.Number);

        if (chapterAlreadyExists)
        {
            throw new ApplicationValidationException("A chapter with this number already exists for this manga.");
        }

        var chapter = new Domain.Entities.Chapter(
            mangaId: mangaId,
            number: request.Number,
            title: request.Title
        );

        var createdChapter = await _chapterRepository.CreateAsync(chapter);

        return new CreateChapterResponse
        {
          Id = createdChapter.Id,
          MangaId = createdChapter.MangaId,
          Number = createdChapter.Number,
          Title = createdChapter.Title  
        };
    }
}