using MangaShelf.Application.ChapterPages;
using MangaShelf.Domain.Entities;
using MangaShelf.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MangaShelf.Infrastructure.Repositories;

public class EfCoreChapterPageRepository : IChapterPageRepository
{
    private readonly MangaShelfDbContext _dbContext;

    public EfCoreChapterPageRepository(MangaShelfDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ChapterPage>> GetByChapterReleaseIdAsync(int chapterReleaseId)
    {
        return await _dbContext.ChapterPages
            .AsNoTracking()
            .Where(p => p.ChapterReleaseId == chapterReleaseId)
            .OrderBy(p => p.PageNumber)
            .ToListAsync();
    }

    public async Task<bool> ExistsByReleaseIdAndPageNumberAsync(int chapterReleaseId, int pageNumber)
    {
        return await _dbContext.ChapterPages
            .AsNoTracking()
            .AnyAsync(p => p.ChapterReleaseId == chapterReleaseId
                && p.PageNumber == pageNumber);
    }

    public async Task<ChapterPage> CreateAsync(ChapterPage chapterPage)
    {
        _dbContext.ChapterPages.Add(chapterPage);

        await _dbContext.SaveChangesAsync();

        return chapterPage;
    }
}