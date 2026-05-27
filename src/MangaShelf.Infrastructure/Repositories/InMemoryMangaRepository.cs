using MangaShelf.Application.Manga;
using MangaShelf.Domain.Entities;
using MangaShelf.Domain.Enums;

namespace MangaShelf.Infrastructure.Repositories;

public class InMemoryMangaRepository : IMangaRepository
{
    private static readonly List<Manga> _manga = new List<Manga>
    {
        new(
            id: 1,
            title: "Berserk", 
            status: MangaStatus.Reading,
            author: "Kentaro Miura",
            artist: "Kentaro Miura", 
            description: "A dark fantasy manga following Guts, a lone swordsman marked by tragedy and revenge."
        ),
        new(
            id: 2,
            title: "Vagabond", 
            status: MangaStatus.PlanToRead, 
            author: "Takehiko Inoue",
            artist: "Takehiko Inoue",
            description: "A historical samurai manga inspired by the life of Miyamoto Musashi."
        ),
        new(
            id: 3,
            title: "Vinland Saga",
            status: MangaStatus.Completed,
            author: "Makoto Yukimura",
            artist: "Makoto Yukimura",
            description: "A historical manga about war, revenge, slavery, and the search for a peaceful land."
        )
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