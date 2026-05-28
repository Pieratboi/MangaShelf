using MangaShelf.Application.ChapterReleases.Create;
using MangaShelf.Application.ChapterReleases.GetByChapter;
using Microsoft.AspNetCore.Mvc;

namespace MangaShelf.Api.Controllers;

[ApiController]
[Route("api/chapters/{chapterId:int}/releases")]
public class ChapterReleasesController : ControllerBase
{
    private readonly CreateChapterReleaseUseCase _createChapterReleaseUseCase;
    private readonly GetChapterReleasesUseCase _getChapterReleasesUseCase;

    public ChapterReleasesController(
        CreateChapterReleaseUseCase createChapterReleaseUseCase,
        GetChapterReleasesUseCase getChapterReleasesUseCase)
    {
        _createChapterReleaseUseCase = createChapterReleaseUseCase;
        _getChapterReleasesUseCase = getChapterReleasesUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetByChapter(int chapterId)
    {
        var releases = await _getChapterReleasesUseCase.ExecuteAsync(chapterId);

        if(releases is null)
        {
            return NotFound();
        }

        return Ok(releases);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        int chapterId,
        [FromBody] CreateChapterReleaseRequest request)
    {
        var release = await _createChapterReleaseUseCase.ExecuteAsync(chapterId, request);

        if(release is null)
        {
            return NotFound();
        }

        return CreatedAtAction(
            nameof(GetByChapter),
            new {chapterId = release.ChapterId},
            release);
    }
}