using ChapterEntity = MangaShelf.Domain.Entities.Chapter;

namespace MangaShelf.Application.Chapters;

public interface IChapterRepository
{
    Task<ChapterEntity?> GetByIdAsync(int id);

    Task<List<ChapterEntity>> GetByMangaIdAsync(int mangaId);

    Task<bool> ExistsByMangaIdAndNumberAsync(int mangaId, int number);

    Task<ChapterEntity> CreateAsync(ChapterEntity chapter);
}