using MangaShelf.Application.Common.Exceptions;
using MangaShelf.Domain.Entities;

namespace MangaShelf.Application.ChapterPages.Delete;

public class DeleteChapterPageUseCase
{
    private readonly IChapterPageRepository _chapterPageRepository;

    public DeleteChapterPageUseCase(IChapterPageRepository chapterPageRepository)
    {
        _chapterPageRepository = chapterPageRepository;
    }

    public async Task<bool> ExecuteAsync(int chapterReleaseId, int pageId)
    {
        if(chapterReleaseId <= 0)
        {
            throw new ApplicationValidationException("Chapter release id must be greater than 0.");
        }

        if(pageId <= 0)
        {
            throw new ApplicationValidationException("Page id mustbe greater than 0.");
        }

        var page = await _chapterPageRepository.GetByIdForUpdateAsync(pageId);

        if(page is null || page.ChapterReleaseId != chapterReleaseId)
        {
            return false;
        }

        _chapterPageRepository.Delete(page);

        await _chapterPageRepository.SaveChangesAsync();

        return true;
    }
}