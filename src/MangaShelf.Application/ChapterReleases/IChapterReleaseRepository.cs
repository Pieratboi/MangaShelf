using ChapterReleaseEntity = MangaShelf.Domain.Entities.ChapterRelease;

namespace MangaShelf.Application.ChapterReleases;

public interface IChapterReleaseRepository
{
    Task<List<ChapterReleaseEntity>> GetByChapterIdAsync(int chapterId);

    Task<bool> ExistsByChapterIdAndScanlatorIdAsync(int chapterId, int scanlatorId);

    Task<ChapterReleaseEntity> CreateAsync(ChapterReleaseEntity chapterRelease);
}