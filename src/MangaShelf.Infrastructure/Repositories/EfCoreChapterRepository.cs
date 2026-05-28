using MangaShelf.Application.Chapters;
using MangaShelf.Domain.Entities;
using MangaShelf.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MangaShelf.Infrastructure.Repositories;

public class EfCoreChapterRepository : IChapterRepository
{
    private readonly MangaShelfDbContext _dbContext;

    public EfCoreChapterRepository(MangaShelfDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Chapter>> GetByMangaIdAsync(int mangaId)
    {
        return await _dbContext.Chapters
            .AsNoTracking()
            .Where(c => c.MangaId == mangaId)
            .OrderBy(c => c.Number)
            .ToListAsync();
    }

    public async Task<bool> ExistsByMangaIdAndNumberAsync(int mangaId, int number)
    {
        return await _dbContext.Chapters
            .AsNoTracking()
            .AnyAsync(c => c.MangaId == mangaId && c.Number == number);
    }

    public async Task<Chapter> CreateAsync(Chapter chapter)
    {
        _dbContext.Chapters.Add(chapter);

        await _dbContext.SaveChangesAsync();

        return chapter;
    }
}