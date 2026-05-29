using MangaShelf.Application.Common.Exceptions;

namespace MangaShelf.Application.Chapters.Delete;

public class DeleteChapterUseCase
{
    private readonly IChapterRepository _chapterRepository;

    public DeleteChapterUseCase(IChapterRepository chapterRepository)
    {
        _chapterRepository = chapterRepository;
    }

    public async Task<bool> ExecuteAsync(int mangaId, int chapterId)
    {
        if(mangaId <= 0)
        {
            throw new ApplicationValidationException("Manga id must be greater than 0.");
        }

        if(chapterId <= 0)
        {
            throw new ApplicationValidationException("Chapter id must be greater than 0.");
        }

        var chapter = await _chapterRepository.GetByIdForUpdateAsync(chapterId);

        if(chapter is null || chapter.MangaId != mangaId)
        {
            return false;
        }

        _chapterRepository.Delete(chapter);

        await _chapterRepository.SaveChangesAsync();

        return true;
    }
}