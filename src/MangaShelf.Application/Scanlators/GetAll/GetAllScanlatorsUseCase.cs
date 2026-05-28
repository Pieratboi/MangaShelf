namespace MangaShelf.Application.Scanlators.GetAll;

public class GetAllScanlatorsUseCase
{
    private readonly IScanlatorRepository _scanlatorRepository;

    public GetAllScanlatorsUseCase(IScanlatorRepository scanlatorRepository)
    {
        _scanlatorRepository = scanlatorRepository;
    }

    public async Task<List<ScanlatorResponse>> ExecuteAsync()
    {
        var scanlators = await _scanlatorRepository.GetAllAsync();
        
        return scanlators.Select(s => new ScanlatorResponse
        {
            Id = s.Id,
            Name = s.Name,
            WebsiteUrl = s.WebsiteUrl
        }).ToList();
    }
}