using MangaShelf.Application.Common.Exceptions;

namespace MangaShelf.Application.Scanlators.Create;

public class CreateScanlatorUseCase
{
    private readonly IScanlatorRepository _scanlatorRepository;

    public CreateScanlatorUseCase(IScanlatorRepository scanlatorRepository)
    {
        _scanlatorRepository = scanlatorRepository;
    }

    public async Task<CreateScanlatorResponse> ExecuteAsync(CreateScanlatorRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ApplicationValidationException("Scanlator name is required.");
        }

        var scanlatorAlreadyExists = await _scanlatorRepository.ExistsByNameAsync(request.Name);

        if (scanlatorAlreadyExists)
        {
            throw new ApplicationValidationException("Scanlator with this name already exists.");
        }

        var scanlator = new Domain.Entities.Scanlator(
            name: request.Name,
            websiteUrl: request.WebsiteUrl);
        
        var createdScanlator = await _scanlatorRepository.CreateAsync(scanlator);

        return new CreateScanlatorResponse
        {
            Id = createdScanlator.Id,
            Name = createdScanlator.Name,
            WebsiteUrl = createdScanlator.WebsiteUrl
        };
    }
}