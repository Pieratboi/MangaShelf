using Microsoft.AspNetCore.Mvc;
using MangaShelf.Application.Manga.GetAll;
using MangaShelf.Application.Manga.GetById;

namespace MangaShelf.Api.Controllers;

[ApiController]
[Route("api/manga")]
public class MangaController : ControllerBase
{
    private readonly GetAllMangaUseCase _getAllMangaUseCase;
    private readonly GetMangaByIdUseCase _getMangaByIdUseCase;

    public MangaController(GetAllMangaUseCase getAllMangaUseCase, 
    GetMangaByIdUseCase getMangaByIdUseCase)
    {
        _getAllMangaUseCase = getAllMangaUseCase;
        _getMangaByIdUseCase = getMangaByIdUseCase;
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
}
