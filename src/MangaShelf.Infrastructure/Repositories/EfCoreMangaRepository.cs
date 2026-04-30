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

    public List<Manga> GetAll()
    {
        return _dbContext.Manga
            .AsNoTracking()
            .OrderBy(m => m.Id)
            .ToList();
    }

    public Manga? GetById(int id)
    {
        return _dbContext.Manga
            .AsNoTracking()
            .FirstOrDefault(m => m.Id == id);
    }

    public Manga Create(Manga manga)
    {
        _dbContext.Manga.Add(manga);
        _dbContext.SaveChanges();

        return manga;
    }
}