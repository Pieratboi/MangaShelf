using MangaShelf.Application.Common.Exceptions;

namespace MangaShelf.Application.ChapterReleases.Delete;

public class DeleteChapterReleaseUseCase
{
    private readonly IChapterReleaseRepository _chapterReleaseRepository;

    public DeleteChapterReleaseUseCase(IChapterReleaseRepository chapterReleaseRepository)
    {
        _chapterReleaseRepository = chapterReleaseRepository;
    }

    public async Task<bool> ExecuteAsync(int chapterId, int releaseId)
    {
        if (chapterId <= 0)
        {
            throw new ApplicationValidationException("Chapter id must be greater than zero.");
        }

        if (releaseId <= 0)
        {
            throw new ApplicationValidationException("Chapter release id must be greater than zero.");
        }

        var release = await _chapterReleaseRepository.GetByIdForUpdateAsync(releaseId);

        if(release is null || release.ChapterId != chapterId)
        {
            return false;
        }

        _chapterReleaseRepository.Delete(release);

        await _chapterReleaseRepository.SaveChangesAsync();

        return true;
    }
}