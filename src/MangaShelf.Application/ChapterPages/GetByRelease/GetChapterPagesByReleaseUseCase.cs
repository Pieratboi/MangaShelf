using MangaShelf.Application.Common.Exceptions;
using MangaShelf.Application.ChapterReleases;

namespace MangaShelf.Application.ChapterPages.GetByRelease;

public class GetChapterPagesByReleaseUseCase
{
    private readonly IChapterPageRepository _chapterPageRepository;
    private readonly IChapterReleaseRepository _chapterReleaseRepository;

    public GetChapterPagesByReleaseUseCase(
        IChapterPageRepository chapterPageRepository,
        IChapterReleaseRepository chapterReleaseRepository)
    {
        _chapterPageRepository = chapterPageRepository;
        _chapterReleaseRepository = chapterReleaseRepository;
    }

    public async Task<List<ChapterPageResponse>?> ExecuteAsync(int chapterReleaseId)
    {
        if(chapterReleaseId <= 0)
        {
            throw new ApplicationValidationException("Chapter release id must be greater than 0.");
        }

        var chapterRelease = await _chapterReleaseRepository.GetByIdAsync(chapterReleaseId);

        if(chapterRelease is null)
        {
            return null;
        }

        var pages = await _chapterPageRepository.GetByChapterReleaseIdAsync(chapterReleaseId);

        return pages.Select(p => new ChapterPageResponse
        {
            Id = p.Id,
            ChapterReleaseId = p.ChapterReleaseId,
            PageNumber = p.PageNumber,
            ImageUrl = p.ImageUrl
        }).ToList();
    }
}