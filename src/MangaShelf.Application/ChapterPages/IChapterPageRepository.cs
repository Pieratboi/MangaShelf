using ChapterPageEntity = MangaShelf.Domain.Entities.ChapterPage;

namespace MangaShelf.Application.ChapterPages;

public interface IChapterPageRepository
{
    Task<List<ChapterPageEntity>> GetByChapterReleaseIdAsync(int chapterReleaseId);

    Task<bool> ExistsByReleaseIdAndPageNumberAsync(int chapterReleaseId, int pageNumber);

    Task<ChapterPageEntity> CreateAsync(ChapterPageEntity chapterPage); 
}