using MangaShelf.Application.Manga;
using MangaShelf.Domain.Entities;
using MangaShelf.Domain.Enums;

namespace MangaShelf.Infrastructure.Repositories;

public class InMemoryMangaRepository : IMangaRepository
{
    private readonly List<Manga> _manga = new List<Manga>
    {
        new(1,"Berserk", MangaStatus.Reading),
        new(2,"Vagabond", MangaStatus.PlanToRead),
        new(3,"Vinland Saga", MangaStatus.Completed)
    };
    
    public List<Manga> GetAll()
    {
        return _manga;
    }

    public Manga? GetById(int id)
    {
        return _manga.FirstOrDefault(m => m.Id == id);
    }
}