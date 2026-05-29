using MangaShelf.Application.ChapterReleases;
using MangaShelf.Domain.Entities;
using MangaShelf.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MangaShelf.Infrastructure.Repositories;

public class EfCoreChapterReleaseRepository : IChapterReleaseRepository
{
    private readonly MangaShelfDbContext _dbContext;

    public EfCoreChapterReleaseRepository(MangaShelfDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ChapterRelease?> GetByIdAsync(int id)
    {
        return await _dbContext.ChapterReleases
            .AsNoTracking()
            .Include(r => r.Scanlator)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<ChapterRelease?> GetByIdForUpdateAsync(int id)
    {
        return await _dbContext.ChapterReleases
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<ChapterRelease>> GetByChapterIdAsync(int chapterId)
    {
        return await _dbContext.ChapterReleases
            .AsNoTracking()
            .Include(r => r.Scanlator)
            .Where(r => r.ChapterId == chapterId)
            .OrderBy(r => r.Scanlator.Name)
            .ToListAsync();
    }

    public async Task<bool> ExistsByChapterIdAndScanlatorIdAsync(int chapterId, int scanlatorId)
    {
        return await _dbContext.ChapterReleases
            .AsNoTracking()
            .AnyAsync(r => r.ChapterId == chapterId && r.ScanlatorId == scanlatorId);
    }

    public async Task<bool> ExistsByChapterIdAndScanlatorIdExceptReleaseAsync(
        int chapterId,
        int scanlatorId,
        int releaseId)
    {
        return await _dbContext.ChapterReleases
            .AsNoTracking()
            .AnyAsync(r => r.ChapterId == chapterId
                && r.ScanlatorId == scanlatorId
                && r.Id != releaseId);
    }

    public async Task<ChapterRelease> CreateAsync(ChapterRelease chapterRelease)
    {
        _dbContext.ChapterReleases.Add(chapterRelease);

        await _dbContext.SaveChangesAsync();

        await _dbContext.Entry(chapterRelease)
            .Reference(r => r.Scanlator)
            .LoadAsync();

        return chapterRelease;
    }

    public void Delete(ChapterRelease chapterRelease)
    {
        _dbContext.ChapterReleases.Remove(chapterRelease);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}