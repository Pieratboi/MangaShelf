using MangaShelf.Application.Manga;
using MangaShelf.Domain.Entities;
using MangaShelf.Domain.Enums;

namespace MangaShelf.Infrastructure.Repositories;

public class InMemoryMangaRepository : IMangaRepository
{
    private static readonly List<Manga> _manga = new List<Manga>
    {
        new(1,"Berserk", MangaStatus.Reading),
        new(2,"Vagabond", MangaStatus.PlanToRead),
        new(3,"Vinland Saga", MangaStatus.Completed)
    };

    public Task<List<Manga>> GetAllAsync()
    {
        return Task.FromResult(_manga.ToList());
    }

    public Task<Manga?> GetByIdAsync(int id)
    {
        var manga = _manga.FirstOrDefault(m => m.Id == id);

        return Task.FromResult(manga);
    }

    public Task<Manga?> GetByIdForUpdateAsync(int id)
    {
        var manga = _manga.FirstOrDefault(m => m.Id == id);

        return Task.FromResult(manga);
    }

    public Task<Manga> CreateAsync(Manga manga)
    {
        _manga.Add(manga);

        return Task.FromResult(manga);
    }

    public void Delete(Manga manga)
    {
        _manga.Remove(manga);
    }

    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
}