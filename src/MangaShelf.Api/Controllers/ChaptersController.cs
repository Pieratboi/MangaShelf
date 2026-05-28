using MangaShelf.Application.Chapters.Create;
using MangaShelf.Application.Chapters.GetByManga;
using Microsoft.AspNetCore.Mvc;

namespace MangaShelf.Api.Controllers;

[ApiController]
[Route("api/manga/{mangaId:int}/chapters")]
public class ChaptersController : ControllerBase
{
    private readonly CreateChapterUseCase _createChapterUseCase;
    private readonly GetChaptersByMangaUseCase _getChaptersByMangaUseCase;

    public ChaptersController(
        CreateChapterUseCase createChapterUseCase,
        GetChaptersByMangaUseCase getChaptersByMangaUseCase)
    {
        _createChapterUseCase = createChapterUseCase;
        _getChaptersByMangaUseCase = getChaptersByMangaUseCase;
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
            nameof(GetByManga),
            new {mangaId = chapter.MangaId},
            chapter);
    }

    [HttpGet]
    public async Task<IActionResult> GetByManga(int mangaId)
    {
        var chapters = await _getChaptersByMangaUseCase.ExecuteAsync(mangaId);

        if (chapters is null)
        {
            return NotFound();
        }

        return Ok(chapters);
    }
}