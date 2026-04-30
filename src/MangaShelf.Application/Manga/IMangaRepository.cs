using MangaShelf.Domain.Entities;

namespace MangaShelf.Application.Manga;

public interface IMangaRepository
{
    List<Domain.Entities.Manga> GetAll();

    Domain.Entities.Manga? GetById(int id);
}