using MangaShelf.Application.Manga;
using MangaShelf.Domain.Entities;

namespace MangaShelf.Infrastructure.Repositories;

public class InMemoryMangaRepository : IMangaRepository
{
    public List<Manga> GetAll()
    {
        return new List<Manga>
        {
            new("Berserk", "Reading"),
            new("Vagabond", "PlanToRead"),
            new("Vinland Saga", "Completed")
        };
    }
}