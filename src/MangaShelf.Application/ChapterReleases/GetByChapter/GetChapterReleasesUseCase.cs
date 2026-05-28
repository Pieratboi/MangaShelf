using MangaShelf.Application.Chapters;
using MangaShelf.Application.Common.Exceptions;

namespace MangaShelf.Application.ChapterReleases.GetByChapter;

public class GetChapterReleasesUseCase
{
    private readonly IChapterRepository _chapterRepository;
    private readonly IChapterReleaseRepository _chapterReleaseRepository;

    public GetChapterReleasesUseCase(
        IChapterRepository chapterRepository, 
        IChapterReleaseRepository chapterReleaseRepository)
    {
        _chapterRepository = chapterRepository;
        _chapterReleaseRepository = chapterReleaseRepository;
    }

    public async Task<List<ChapterReleaseResponse>?> ExecuteAsync(int chapterId)
    {
        if(chapterId <= 0)
        {
            throw new ApplicationValidationException("Chapter id must be greater than 0.");
        }

        var chapter = await _chapterRepository.GetByIdAsync(chapterId);

        if(chapter is null)
        {
            return null;
        }

        var releases = await _chapterReleaseRepository.GetByChapterIdAsync(chapterId);

        return releases.Select(r => new ChapterReleaseResponse
        {
            Id = r.Id,
            ChapterId = r.ChapterId,
            ScanlatorId = r.ScanlatorId,
            ScanlatorName = r.Scanlator.Name,
            SourceUrl = r.SourceUrl,
            Language = r.Language
        }).ToList();
    }
}