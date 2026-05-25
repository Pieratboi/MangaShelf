using Microsoft.AspNetCore.Mvc;
using MangaShelf.Application.Manga.GetAll;
using MangaShelf.Application.Manga.GetById;
using MangaShelf.Application.Manga.Create;
using MangaShelf.Application.Manga.UpdateStatus;
using MangaShelf.Application.Manga.Delete;

namespace MangaShelf.Api.Controllers;

[ApiController]
[Route("api/manga")]
public class MangaController : ControllerBase
{
    private readonly GetAllMangaUseCase _getAllMangaUseCase;
    private readonly GetMangaByIdUseCase _getMangaByIdUseCase;
    private readonly CreateMangaUseCase _createMangaUseCase;
    private readonly UpdateMangaStatusUseCase _updateMangaUseCase;
    private readonly DeleteMangaUseCase _deleteMangaUseCase;

    public MangaController(GetAllMangaUseCase getAllMangaUseCase, 
    GetMangaByIdUseCase getMangaByIdUseCase, CreateMangaUseCase createMangaUseCase,
    UpdateMangaStatusUseCase updateMangaUseCase, DeleteMangaUseCase deleteMangaUseCase)
    {
        _getAllMangaUseCase = getAllMangaUseCase;
        _getMangaByIdUseCase = getMangaByIdUseCase;
        _createMangaUseCase = createMangaUseCase;
        _updateMangaUseCase = updateMangaUseCase;
        _deleteMangaUseCase = deleteMangaUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var manga = await _getAllMangaUseCase.ExecuteAsync();

        return Ok(manga);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var manga = await _getMangaByIdUseCase.ExecuteAsync(id);

        if(manga is null)
        {
            return NotFound();
        }

        return Ok(manga);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMangaRequest request)
    {
        var manga = await _createMangaUseCase.ExecuteAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = manga.Id },
            manga
        );
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateMangaStatusRequest request)
    {
        var manga = await _updateMangaUseCase.ExecuteAsync(id, request);

        if (manga is null)
        {
            return NotFound();
        }

        return Ok(manga);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _deleteMangaUseCase.ExecuteAsync(id);

        if(!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
