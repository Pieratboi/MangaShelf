using MangaShelf.Application.Common.Exceptions;

namespace MangaShelf.Application.Chapters.GetById;

public class GetChapterByIdUseCase
{
    private readonly IChapterRepository _chapterRepository;

    public GetChapterByIdUseCase(IChapterRepository chapterRepository)
    {
        _chapterRepository = chapterRepository;
    }

    public async Task<ChapterDetailsResponse?> ExecuteAsync(int mangaId, int chapterId)
    {
        if(mangaId <= 0)
        {
            throw new ApplicationValidationException("Manga id must be greater than 0.");
        }

        if(chapterId <= 0)
        {
            throw new ApplicationValidationException("Chapter id must be greater than 0.");
        }

        var chapter = await _chapterRepository.GetByIdAsync(chapterId);

        if(chapter is null || chapter.MangaId != mangaId)
        {
            return null;
        }

        return new ChapterDetailsResponse
        {
            Id = chapter.Id,
            MangaId = chapter.MangaId,
            Number = chapter.Number,
            Title = chapter.Title
        };
    }
}