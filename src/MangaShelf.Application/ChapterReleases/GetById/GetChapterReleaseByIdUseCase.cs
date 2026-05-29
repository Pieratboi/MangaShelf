using MangaShelf.Application.Common.Exceptions;

namespace MangaShelf.Application.ChapterReleases.GetById;

public class GetChapterReleaseByIdUseCase
{
    private readonly IChapterReleaseRepository _chapterReleaseRepository;

    public GetChapterReleaseByIdUseCase(IChapterReleaseRepository chapterReleaseRepository)
    {
        _chapterReleaseRepository = chapterReleaseRepository;
    }

    public async Task<ChapterReleaseDetailsResponse?> ExecuteAsync(
        int chapterId,
        int releaseId)
    {
        if(chapterId <= 0)
        {
            throw new ApplicationValidationException("Chapter id must be greater than 0.");
        }
        
        if(releaseId <= 0)
        {
            throw new ApplicationValidationException("Chapter release id must be greater than 0.");
        }

        var release = await _chapterReleaseRepository.GetByIdAsync(releaseId);

        if(release is null || release.ChapterId != chapterId)
        {
            return null;
        }

        return new ChapterReleaseDetailsResponse
        {
            Id = release.Id,
            ChapterId = release.ChapterId,
            ScanlatorId = release.ScanlatorId,
            ScanlatorName = release.Scanlator.Name,
            SourceUrl = release.SourceUrl,
            Language = release.Language
        };
    }
}