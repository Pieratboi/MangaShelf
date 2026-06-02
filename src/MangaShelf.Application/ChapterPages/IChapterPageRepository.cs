using ChapterPageEntity = MangaShelf.Domain.Entities.ChapterPage;

namespace MangaShelf.Application.ChapterPages;

public interface IChapterPageRepository
{
    Task<ChapterPageEntity?> GetByIdAsync(int id);

    Task<ChapterPageEntity?> GetByIdForUpdateAsync(int id);

    Task<List<ChapterPageEntity>> GetByChapterReleaseIdAsync(int chapterReleaseId);

    Task<bool> ExistsByReleaseIdAndPageNumberAsync(int chapterReleaseId, int pageNumber);

    Task<bool> ExistsByReleaseIdAndPageNumberExceptPageAsync(
        int chapterReleaseId,
        int pageNumber,
        int pageId);

    Task<ChapterPageEntity> CreateAsync(ChapterPageEntity chapterPage); 

    void Delete(ChapterPageEntity chapterPage);

    Task SaveChangesAsync();
}