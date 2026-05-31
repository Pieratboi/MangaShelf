using MangaShelf.Application.ChapterPages.Create;
using MangaShelf.Application.ChapterPages.GetByRelease;
using Microsoft.AspNetCore.Mvc;

namespace MangaShelf.Api.Controllers;

[ApiController]
[Route("api/chapter-releases/{chapterReleaseId:int}/pages")]
public class ChapterPagesController : ControllerBase
{
    private readonly CreateChapterPageUseCase _createChapterPageUseCase;
    private readonly GetChapterPagesByReleaseUseCase _getChapterPagesByReleaseUseCase;

    public ChapterPagesController(
        CreateChapterPageUseCase createChapterPageUseCase,
        GetChapterPagesByReleaseUseCase getChapterPagesByReleaseUseCase)
    {
        _createChapterPageUseCase = createChapterPageUseCase;
        _getChapterPagesByReleaseUseCase = getChapterPagesByReleaseUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetByRelease(int chapterReleaseId)
    {
        var pages = await _getChapterPagesByReleaseUseCase.ExecuteAsync(chapterReleaseId);

        if(pages is null)
        {
            return NotFound();
        }

        return Ok(pages);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        int chapterReleaseId,
        [FromBody] CreateChapterPageRequest request)
    {
        var page = await _createChapterPageUseCase
            .ExecuteAsync(chapterReleaseId, request);

        if(page is null)
        {
            return NotFound();
        }

        return Created(
            $"/api/chapter-releases/{page.ChapterReleaseId}/pages",
            page);
        // return CreatedAtAction(
        //     nameof(GetByRelease),
        //     new {chapterReleaseId, page.ChapterReleaseId},
        //     page);
    }
}