using MangaShelf.Application.Manga;
using MangaShelf.Domain.Entities;

namespace MangaShelf.Infrastructure.Repositories;

public class InMemoryMangaRepository : IMangaRepository
{
    public List<Manga> GetAll()
    {
        return new List<Manga>
        {
            new(1,"Berserk", "Reading"),
            new(2,"Vagabond", "PlanToRead"),
            new(3,"Vinland Saga", "Completed")
        };
    }
}