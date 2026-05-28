using MangaShelf.Application.Scanlators;
using MangaShelf.Domain.Entities;
using MangaShelf.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MangaShelf.Infrastructure.Repositories;

public class EfCoreScanlatorRepository : IScanlatorRepository
{
    private readonly MangaShelfDbContext _dbContext;

    public EfCoreScanlatorRepository(MangaShelfDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Scanlator>> GetAllAsync()
    {
        return await _dbContext.Scanlators
            .AsNoTracking()
            .OrderBy(s => s.Id)
            .ToListAsync();
    }

    public async Task<Scanlator?> GetByIdAsync(int id)
    {
        return await _dbContext.Scanlators
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        var normalizedName = name.Trim().ToLower();

        return await _dbContext.Scanlators
            .AsNoTracking()
            .AnyAsync(s => s.Name.ToLower() == normalizedName);
    }

    public async Task<Scanlator> CreateAsync(Scanlator scanlator)
    {
        _dbContext.Scanlators.Add(scanlator);

        await _dbContext.SaveChangesAsync();

        return scanlator;
    }
}