using Microsoft.AspNetCore.Mvc;
using MangaShelf.Application.Manga.GetAll;
using MangaShelf.Application.Manga.GetById;
using MangaShelf.Application.Manga.Create;

namespace MangaShelf.Api.Controllers;

[ApiController]
[Route("api/manga")]
public class MangaController : ControllerBase
{
    private readonly GetAllMangaUseCase _getAllMangaUseCase;
    private readonly GetMangaByIdUseCase _getMangaByIdUseCase;
    private readonly CreateMangaUseCase _createMangaUseCase;

    public MangaController(GetAllMangaUseCase getAllMangaUseCase, 
    GetMangaByIdUseCase getMangaByIdUseCase, CreateMangaUseCase createMangaUseCase)
    {
        _getAllMangaUseCase = getAllMangaUseCase;
        _getMangaByIdUseCase = getMangaByIdUseCase;
        _createMangaUseCase = createMangaUseCase;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var manga = _getAllMangaUseCase.Execute();

        return Ok(manga);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var manga = _getMangaByIdUseCase.Execute(id);

        if(manga is null)
        {
            return NotFound();
        }

        return Ok(manga);
    }

    [HttpPost]
    public IActionResult Create(CreateMangaRequest request)
    {
        var manga = _createMangaUseCase.Execute(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = manga.Id },
            manga
        );
    }
}
