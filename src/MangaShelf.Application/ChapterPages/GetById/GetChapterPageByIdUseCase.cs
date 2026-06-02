using MangaShelf.Application.Common.Exceptions;

namespace MangaShelf.Application.ChapterPages.GetById;

public class GetChapterPageByIdUseCase
{
    private readonly IChapterPageRepository _chapterPageRepository;

    public GetChapterPageByIdUseCase(IChapterPageRepository chapterPageRepository)
    {
        _chapterPageRepository = chapterPageRepository;
    }

    public async Task<ChapterPageDetailsResponse?> ExecuteAsync(
        int chapterReleaseId, 
        int pageId)
    {
        if (chapterReleaseId <= 0)
        {
            throw new ApplicationValidationException(
                "Chapter release id must be greater than zero."
            );
        }

        if (pageId <= 0)
        {
            throw new ApplicationValidationException(
                "Chapter page id must be greater than zero."
            );
        }

        var page = await _chapterPageRepository.GetByIdAsync(pageId);

        if(page is null || page.ChapterReleaseId != chapterReleaseId)
        {
            return null;
        }

        return new ChapterPageDetailsResponse
        {
            Id = page.Id,
            ChapterReleaseId = page.ChapterReleaseId,
            PageNumber = page.PageNumber,
            ImageUrl = page.ImageUrl
        };
    }
}