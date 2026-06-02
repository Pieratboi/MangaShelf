using MangaShelf.Application.ChapterPages.Create;
using MangaShelf.Application.ChapterPages.GetByRelease;
using MangaShelf.Application.ChapterPages.GetById;
using MangaShelf.Application.ChapterPages.Update;
using MangaShelf.Application.ChapterPages.Delete;
using Microsoft.AspNetCore.Mvc;

namespace MangaShelf.Api.Controllers;

[ApiController]
[Route("api/chapter-releases/{chapterReleaseId:int}/pages")]
public class ChapterPagesController : ControllerBase
{
    private readonly CreateChapterPageUseCase _createChapterPageUseCase;
    private readonly GetChapterPagesByReleaseUseCase _getChapterPagesByReleaseUseCase;
    private readonly GetChapterPageByIdUseCase _getChapterPageByIdUseCase;
    private readonly UpdateChapterPageUseCase _updateChapterPageUseCase;
    private readonly DeleteChapterPageUseCase _deleteChapterPageUseCase;

    public ChapterPagesController(
        CreateChapterPageUseCase createChapterPageUseCase,
        GetChapterPagesByReleaseUseCase getChapterPagesByReleaseUseCase,
        GetChapterPageByIdUseCase getChapterPageByIdUseCase,
        UpdateChapterPageUseCase updateChapterPageUseCase,
        DeleteChapterPageUseCase deleteChapterPageUseCase)
    {
        _createChapterPageUseCase = createChapterPageUseCase;
        _getChapterPagesByReleaseUseCase = getChapterPagesByReleaseUseCase;
        _getChapterPageByIdUseCase = getChapterPageByIdUseCase;
        _updateChapterPageUseCase = updateChapterPageUseCase;
        _deleteChapterPageUseCase = deleteChapterPageUseCase;
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

    [HttpGet("{pageId:int}")]
    public async Task<IActionResult> GetById(
        int chapterReleaseId,
        int pageId)
    {
        var page = await _getChapterPageByIdUseCase.ExecuteAsync(chapterReleaseId, pageId);

        if(page is null)
        {
            return NotFound();
        }

        return Ok(page);
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

        return CreatedAtAction(
            nameof(GetById),
            new
            {
                chapterReleaseId = page.ChapterReleaseId,
                pageId = page.Id
            },
            page);
    }

    [HttpPatch("{pageId:int}")]
    public async Task<IActionResult> Update(
        int chapterReleaseId,
        int pageId,
        [FromBody] UpdateChapterPageRequest request)
    {
        var page = await _updateChapterPageUseCase
            .ExecuteAsync(chapterReleaseId, pageId, request);

        if (page is null)
        {
            return NotFound();
        }

        return Ok(page);
    }

    [HttpDelete("{pageId:int}")]
    public async Task<IActionResult> Delete(
        int chapterReleaseId,
        int pageId)
    {
        var deleted = await _deleteChapterPageUseCase.ExecuteAsync(
            chapterReleaseId, 
            pageId);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}