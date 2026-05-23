using MangaEntity = MangaShelf.Domain.Entities.Manga;

namespace MangaShelf.Application.Manga;

public interface IMangaRepository
{
    Task<List<MangaEntity>> GetAllAsync();

    Task<MangaEntity?> GetByIdAsync(int id);

    Task<MangaEntity?> GetByIdForUpdateAsync(int id);

    Task<MangaEntity> CreateAsync(MangaEntity manga);

    Task SaveChangesAsync();
}