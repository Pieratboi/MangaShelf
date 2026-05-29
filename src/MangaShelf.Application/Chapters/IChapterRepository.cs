using ChapterEntity = MangaShelf.Domain.Entities.Chapter;

namespace MangaShelf.Application.Chapters;

public interface IChapterRepository
{
    Task<ChapterEntity?> GetByIdAsync(int id);

    Task<ChapterEntity?> GetByIdForUpdateAsync(int id);

    Task<List<ChapterEntity>> GetByMangaIdAsync(int mangaId);

    Task<bool> ExistsByMangaIdAndNumberAsync(int mangaId, int number);

    Task<bool> ExistsByMangaIdAndNumberExceptChapterAsync(
        int mangaId,
        int number,
        int chapterId);

    Task<ChapterEntity> CreateAsync(ChapterEntity chapter);

    void Delete(ChapterEntity chapter);

    Task SaveChangesAsync();
}