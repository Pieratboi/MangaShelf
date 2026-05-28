using ScanlatorEntity = MangaShelf.Domain.Entities.Scanlator;

namespace MangaShelf.Application.Scanlators;

public interface IScanlatorRepository
{
    Task<List<ScanlatorEntity>> GetAllAsync();

    Task<ScanlatorEntity?> GetByIdAsync(int id);

    Task<bool> ExistsByNameAsync(string name);

    Task<ScanlatorEntity> CreateAsync(ScanlatorEntity scanlator);
}