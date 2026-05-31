using MangaShelf.Application.Common.Exceptions;
using MangaShelf.Application.ChapterReleases;

namespace MangaShelf.Application.ChapterPages.Create;

public class CreateChapterPageUseCase
{
    private readonly IChapterPageRepository _chapterPageRepository;
    private readonly IChapterReleaseRepository _chapterReleaseRepository;

    public CreateChapterPageUseCase(
        IChapterPageRepository chapterPageRepository,
        IChapterReleaseRepository chapterReleaseRepository)
    {
        _chapterPageRepository = chapterPageRepository;
        _chapterReleaseRepository = chapterReleaseRepository;
    }

    public async Task<CreateChapterPageResponse?> ExecuteAsync(
        int chapterReleaseId, 
        CreateChapterPageRequest request)
    {
        if(chapterReleaseId <= 0)
        {
            throw new ApplicationValidationException("Chapter release id must be greater than 0.");
        }

        if(request.PageNumber <= 0)
        {
            throw new ApplicationValidationException("Page number must be greater than 0.");
        }

        if (string.IsNullOrWhiteSpace(request.ImageUrl))
        {
            throw new ApplicationValidationException("Page image URL is required.");
        }

        var chapterRelease = await _chapterReleaseRepository.GetByIdAsync(chapterReleaseId);

        if(chapterRelease is null)
        {
            return null;
        }

        var pageAlreadyExists = await _chapterPageRepository
            .ExistsByReleaseIdAndPageNumberAsync(
                chapterReleaseId,
                request.PageNumber);

        if (pageAlreadyExists)
        {
            throw new ApplicationValidationException(
                "A page with this number already exists for this chapter release."
            );
        }


        var page = new Domain.Entities.ChapterPage(
            chapterReleaseId: chapterReleaseId,
            pageNumber: request.PageNumber,
            request.ImageUrl
        );

        var createdPage = await _chapterPageRepository.CreateAsync(page);

        return new CreateChapterPageResponse
        {
            Id = createdPage.Id,
            ChapterReleaseId = createdPage.ChapterReleaseId,
            PageNumber = createdPage.PageNumber,
            ImageUrl = createdPage.ImageUrl
        };
    }
}