using MangaShelf.Application.Manga;
using MangaShelf.Domain.Entities;
using MangaShelf.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MangaShelf.Infrastructure.Repositories;

public class EfCoreMangaRepository : IMangaRepository
{
    private readonly MangaShelfDbContext _dbContext;

    public EfCoreMangaRepository(MangaShelfDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Manga>> GetAllAsync()
    {
        return await _dbContext.Manga
            .AsNoTracking()
            .OrderBy(m => m.Id)
            .ToListAsync();
    }

    public async Task<Manga?> GetByIdAsync(int id)
    {
        return await _dbContext.Manga
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Manga?> GetByIdForUpdateAsync(int id)
    {
        return await _dbContext.Manga
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Manga> CreateAsync(Manga manga)
    {
        _dbContext.Manga.Add(manga);

        await _dbContext.SaveChangesAsync();

        return manga;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}