using MangaShelf.Application.Common.Exceptions;
using MangaShelf.Application.Scanlators;

namespace MangaShelf.Application.ChapterReleases.Update;

public class UpdateChapterReleaseUseCase
{
    private readonly IChapterReleaseRepository _chapterReleaseRepository;
    private readonly IScanlatorRepository _scanlatorRepository;

    public UpdateChapterReleaseUseCase(
        IChapterReleaseRepository chapterReleaseRepository,
        IScanlatorRepository scanlatorRepository)
    {
        _chapterReleaseRepository = chapterReleaseRepository;
        _scanlatorRepository = scanlatorRepository;
    }

    public async Task<UpdateChapterReleaseResponse?> ExecuteAsync(
        int chapterId,
        int releaseId,
        UpdateChapterReleaseRequest request)
    {
        if (chapterId <= 0)
        {
            throw new ApplicationValidationException("Chapter id must be greater than zero.");
        }

        if (releaseId <= 0)
        {
            throw new ApplicationValidationException("Chapter release id must be greater than zero.");
        }

        if(request.ScanlatorId is null
            && request.SourceUrl is null
            && request.Language is null)
        {
            throw new ApplicationValidationException("At least one chapter release detail must be provided.");
        }

        var release = await _chapterReleaseRepository.GetByIdForUpdateAsync(releaseId);

        if(release is null || release.ChapterId != chapterId)
        {
            return null;
        }

        if(request.ScanlatorId is not null)
        {
            if(request.ScanlatorId.Value <= 0)
            {
                throw new ApplicationValidationException("Scanlator id must be greater than 0.");
            }

            var scanlator = await _scanlatorRepository.GetByIdAsync(request.ScanlatorId.Value);

            if(scanlator is null)
            {
                throw new ApplicationValidationException("Scanlator does not exist.");
            }

            var duplicateExist = await _chapterReleaseRepository
                .ExistsByChapterIdAndScanlatorIdExceptReleaseAsync(
                    chapterId,
                    request.ScanlatorId.Value,
                    releaseId);

            if (duplicateExist)
            {
                throw new ApplicationValidationException("This scanlator already has a release for this chapter.");
            }

            release.ChangeScanlatorId(request.ScanlatorId.Value);
        }

        if(request.SourceUrl is not null)
        {
            release.ChangeSourceUrl(request.SourceUrl);
        }

        if(request.Language is not null)
        {
            release.ChangeLanguage(request.Language);
        }

        await _chapterReleaseRepository.SaveChangesAsync();

        var updatedRelease = await _chapterReleaseRepository.GetByIdAsync(releaseId);

        if(updatedRelease is null)
        {
            return null;
        }

        return new UpdateChapterReleaseResponse
        {
            Id = updatedRelease.Id,
            ChapterId = updatedRelease.ChapterId,
            ScanlatorId = updatedRelease.ScanlatorId,
            ScanlatorName = updatedRelease.Scanlator.Name,
            SourceUrl = updatedRelease.SourceUrl,
            Language = updatedRelease.Language
        };
    }
}