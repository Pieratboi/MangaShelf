using Microsoft.AspNetCore.Mvc;
using MangaShelf.Application.Manga.GetAll;

namespace MangaShelf.Api.Controllers;

[ApiController]
[Route("api/manga")]
public class MangaController : ControllerBase
{
    private readonly GetAllMangaUseCase _getAllMangaUseCase;

    public MangaController(GetAllMangaUseCase getAllMangaUseCase)
    {
        _getAllMangaUseCase = getAllMangaUseCase;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var manga = _getAllMangaUseCase.Execute();

        return Ok(manga);
    }
}
