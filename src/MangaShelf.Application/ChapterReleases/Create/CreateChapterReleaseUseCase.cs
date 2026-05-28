using MangaShelf.Application.Chapters;
using MangaShelf.Application.Common.Exceptions;
using MangaShelf.Application.Scanlators;

namespace MangaShelf.Application.ChapterReleases.Create;

public class CreateChapterReleaseUseCase
{
    private readonly IChapterRepository _chapterRepository;
    private readonly IScanlatorRepository _scanlatorRepository;
    private readonly IChapterReleaseRepository _chapterReleaseRepository;

    public CreateChapterReleaseUseCase(
        IChapterRepository chapterRepository,
        IScanlatorRepository scanlatorRepository,
        IChapterReleaseRepository chapterReleaseRepository)
    {
        _chapterRepository = chapterRepository;
        _scanlatorRepository = scanlatorRepository;
        _chapterReleaseRepository = chapterReleaseRepository;
    }

    public async Task<CreateChapterReleaseResponse?> ExecuteAsync(
        int chapterId, 
        CreateChapterReleaseRequest request)
    {
        if(chapterId <= 0)
        {
            throw new ApplicationValidationException("Chapter id must be greater than 0.");
        }

        if(request.ScanlatorId <= 0)
        {
            throw new ApplicationValidationException("Scanlator id must be greater than 0.");
        }

        var chapter = await _chapterRepository.GetByIdAsync(chapterId);

        if(chapter is null)
        {
            return null;
        }

        var scanlator = await _scanlatorRepository.GetByIdAsync(chapterId);

        if(scanlator is null)
        {
            return null;
        }

        var releaseAlreadyExists = await _chapterReleaseRepository
            .ExistsByChapterIdAndScanlatorIdAsync(chapterId, request.ScanlatorId);

        if (releaseAlreadyExists)
        {
            throw new ApplicationValidationException("This scanlator already has a release for this chapter.");
        }

        var chapterRelease = new Domain.Entities.ChapterRelease(
            chapterId: chapterId,
            scanlatorId: request.ScanlatorId,
            sourceUrl: request.SourceUrl,
            language: request.Language
        );

        var createdRelease = await _chapterReleaseRepository.CreateAsync(chapterRelease);

        return new CreateChapterReleaseResponse
        {
            Id = createdRelease.Id,
            ChapterId = createdRelease.ChapterId,
            ScanlatorId = createdRelease.ScanlatorId,
            ScanlatorName = createdRelease.Scanlator.Name,
            SourceUrl = createdRelease.SourceUrl,
            Language = createdRelease.Language
        };
    }
}