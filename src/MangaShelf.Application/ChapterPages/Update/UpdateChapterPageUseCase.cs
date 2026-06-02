using MangaShelf.Application.Common.Exceptions;

namespace MangaShelf.Application.ChapterPages.Update;

public class UpdateChapterPageUseCase
{
    private readonly IChapterPageRepository _chapterPageRepository;

    public UpdateChapterPageUseCase(IChapterPageRepository chapterPageRepository)
    {
        _chapterPageRepository = chapterPageRepository;
    }

    public async Task<UpdateChapterPageResponse?> ExecuteAsync(
        int chapterReleaseId,
        int pageId,
        UpdateChapterPageRequest request)
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

        if (request.PageNumber is null && request.ImageUrl is null)
        {
            throw new ApplicationValidationException(
                "At least one chapter page detail must be provided."
            );
        }

        var page = await _chapterPageRepository.GetByIdForUpdateAsync(pageId);

        if(page is null || page.ChapterReleaseId != chapterReleaseId)
        {
            return null;
        }

        if(request.PageNumber is not null)
        {
            var duplicateExists = await _chapterPageRepository
                .ExistsByReleaseIdAndPageNumberExceptPageAsync(
                    chapterReleaseId,
                    request.PageNumber.Value,
                    pageId);

            if (duplicateExists)
            {
                throw new ApplicationValidationException(
                    "A page with this number already exists for this chapter release."
                );
            }

            page.ChangePageNumber(request.PageNumber.Value);
        }

        if(request.ImageUrl is not null)
        {
            page.ChangeImageUrl(request.ImageUrl);
        }

        await _chapterPageRepository.SaveChangesAsync();

        return new UpdateChapterPageResponse
        {
            Id = page.Id,
            ChapterReleaseId = page.ChapterReleaseId,
            PageNumber = page.PageNumber,
            ImageUrl = page.ImageUrl
        };
    }
}