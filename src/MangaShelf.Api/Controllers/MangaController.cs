using Microsoft.AspNetCore.Mvc;
using MangaShelf.Application.Manga.GetAll;
using MangaShelf.Application.Manga.GetById;
using MangaShelf.Application.Manga.Create;
using MangaShelf.Application.Manga.UpdateStatus;

namespace MangaShelf.Api.Controllers;

[ApiController]
[Route("api/manga")]
public class MangaController : ControllerBase
{
    private readonly GetAllMangaUseCase _getAllMangaUseCase;
    private readonly GetMangaByIdUseCase _getMangaByIdUseCase;
    private readonly CreateMangaUseCase _createMangaUseCase;
    private readonly UpdateMangaUseCase _updateMangaUseCase;

    public MangaController(GetAllMangaUseCase getAllMangaUseCase, 
    GetMangaByIdUseCase getMangaByIdUseCase, CreateMangaUseCase createMangaUseCase,
    UpdateMangaUseCase updateMangaUseCase)
    {
        _getAllMangaUseCase = getAllMangaUseCase;
        _getMangaByIdUseCase = getMangaByIdUseCase;
        _createMangaUseCase = createMangaUseCase;
        _updateMangaUseCase = updateMangaUseCase;
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
}
