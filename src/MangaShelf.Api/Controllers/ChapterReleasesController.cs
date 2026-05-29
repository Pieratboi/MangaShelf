using MangaShelf.Application.ChapterReleases.Create;
using MangaShelf.Application.ChapterReleases.GetByChapter;
using MangaShelf.Application.ChapterReleases.GetById;
using MangaShelf.Application.ChapterReleases.Update;
using MangaShelf.Application.ChapterReleases.Delete;
using Microsoft.AspNetCore.Mvc;

namespace MangaShelf.Api.Controllers;

[ApiController]
[Route("api/chapters/{chapterId:int}/releases")]
public class ChapterReleasesController : ControllerBase
{
    private readonly CreateChapterReleaseUseCase _createChapterReleaseUseCase;
    private readonly GetChapterReleasesUseCase _getChapterReleasesUseCase;
    private readonly GetChapterReleaseByIdUseCase _getChapterReleaseByIdUseCase;
    private readonly UpdateChapterReleaseUseCase _updateChapterReleaseUseCase;
    private readonly DeleteChapterReleaseUseCase _deleteChapterReleaseUseCase;

    public ChapterReleasesController(
        CreateChapterReleaseUseCase createChapterReleaseUseCase,
        GetChapterReleasesUseCase getChapterReleasesUseCase,
        GetChapterReleaseByIdUseCase getChapterReleaseByIdUseCase,
        UpdateChapterReleaseUseCase updateChapterReleaseUseCase,
        DeleteChapterReleaseUseCase deleteChapterReleaseUseCase)
    {
        _createChapterReleaseUseCase = createChapterReleaseUseCase;
        _getChapterReleasesUseCase = getChapterReleasesUseCase;
        _getChapterReleaseByIdUseCase = getChapterReleaseByIdUseCase;
        _updateChapterReleaseUseCase = updateChapterReleaseUseCase;
        _deleteChapterReleaseUseCase = deleteChapterReleaseUseCase;
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

    [HttpGet("{releaseId:int}")]
    public async Task<IActionResult> GetById(int chapterId, int releaseId)
    {
        var release = await _getChapterReleaseByIdUseCase.ExecuteAsync(
            chapterId,
            releaseId);

        if (release is null)
        {
            return NotFound();
        }

        return Ok(release);
    }

    [HttpPatch("{releaseId:int}")]
    public async Task<IActionResult> Update(
        int chapterId,
        int releaseId,
        [FromBody] UpdateChapterReleaseRequest request)
    {
        var release = await _updateChapterReleaseUseCase.ExecuteAsync(
            chapterId,
            releaseId,
            request);

        if (release is null)
        {
            return NotFound();
        }

        return Ok(release);
    }

    [HttpDelete("{releaseId:int}")]
    public async Task<IActionResult> Delete(int chapterId, int releaseId)
    {
        var deleted = await _deleteChapterReleaseUseCase.ExecuteAsync(
            chapterId,
            releaseId);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}