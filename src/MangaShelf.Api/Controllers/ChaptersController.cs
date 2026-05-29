using MangaShelf.Application.Chapters.Create;
using MangaShelf.Application.Chapters.GetByMangaId;
using MangaShelf.Application.Chapters.GetById;
using MangaShelf.Application.Chapters.Update;
using MangaShelf.Application.Chapters.Delete;
using Microsoft.AspNetCore.Mvc;

namespace MangaShelf.Api.Controllers;

[ApiController]
[Route("api/manga/{mangaId:int}/chapters")]
public class ChaptersController : ControllerBase
{
    private readonly CreateChapterUseCase _createChapterUseCase;
    private readonly GetChaptersByMangaIdUseCase _getChaptersByMangaUseCase;
    private readonly GetChapterByIdUseCase _getChapterByIdUseCase;
    private readonly UpdateChapterUseCase _updateChapterUseCase;
    private readonly DeleteChapterUseCase _deleteChapterUseCase;

    public ChaptersController(
        CreateChapterUseCase createChapterUseCase,
        GetChaptersByMangaIdUseCase getChaptersByMangaIdUseCase,
        GetChapterByIdUseCase getChapterByIdUseCase,
        UpdateChapterUseCase updateChapterUseCase,
        DeleteChapterUseCase deleteChapterUseCase)
    {
        _createChapterUseCase = createChapterUseCase;
        _getChaptersByMangaUseCase = getChaptersByMangaIdUseCase;
        _getChapterByIdUseCase = getChapterByIdUseCase;
        _updateChapterUseCase = updateChapterUseCase;
        _deleteChapterUseCase = deleteChapterUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create(int mangaId, [FromBody] CreateChapterRequest request)
    {
        var chapter = await _createChapterUseCase.ExecuteAsync(mangaId, request);

        if (chapter is null)
        {
            return NotFound();
        }

        return CreatedAtAction(
            nameof(GetByMangaId),
            new {mangaId = chapter.MangaId},
            chapter);
    }

    [HttpGet]
    public async Task<IActionResult> GetByMangaId(int mangaId)
    {
        var chapters = await _getChaptersByMangaUseCase.ExecuteAsync(mangaId);

        if (chapters is null)
        {
            return NotFound();
        }

        return Ok(chapters);
    }

    [HttpGet("{chapterId:int}")]
    public async Task<IActionResult> GetById(int mangaId, int chapterId)
    {
        var chapter = await _getChapterByIdUseCase.ExecuteAsync(mangaId, chapterId);

        if(chapter is null)
        {
            return NotFound();
        }

        return Ok(chapter);
    }

    [HttpPatch("{chapterId:int}")]
    public async Task<IActionResult> Update(
        int mangaId, 
        int chapterId, 
        [FromBody] UpdateChapterRequest request)
    {
        var chapter = await _updateChapterUseCase.
            ExecuteAsync(mangaId, chapterId, request);
        
        if(chapter is null)
        {
            return NotFound();
        }

        return Ok(chapter);
    }

    [HttpDelete("{chapterId:int}")]
    public async Task<IActionResult> Delete(int mangaId, int chapterId)
    {
        var deleted = await _deleteChapterUseCase.ExecuteAsync(mangaId, chapterId);

        if(deleted is false)
        {
            return NotFound();
        }

        return NoContent();
    }
}