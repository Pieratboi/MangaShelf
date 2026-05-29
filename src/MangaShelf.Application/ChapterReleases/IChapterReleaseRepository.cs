using ChapterReleaseEntity = MangaShelf.Domain.Entities.ChapterRelease;

namespace MangaShelf.Application.ChapterReleases;

public interface IChapterReleaseRepository
{
    Task<ChapterReleaseEntity?> GetByIdAsync(int id);

    Task<ChapterReleaseEntity?> GetByIdForUpdateAsync(int id);

    Task<List<ChapterReleaseEntity>> GetByChapterIdAsync(int chapterId);

    Task<bool> ExistsByChapterIdAndScanlatorIdAsync(int chapterId, int scanlatorId);

    Task<bool> ExistsByChapterIdAndScanlatorIdExceptReleaseAsync(
        int chapterId,
        int scanlatorId,
        int releaseId);

    Task<ChapterReleaseEntity> CreateAsync(ChapterReleaseEntity chapterRelease);

    void Delete(ChapterReleaseEntity chapterRelease);

    Task SaveChangesAsync();
}