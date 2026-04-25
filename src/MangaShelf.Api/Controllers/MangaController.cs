using Microsoft.AspNetCore.Mvc;
using MangaShelf.Domain.Entities;

namespace MangaShelf.Api.Controllers;

[ApiController]
[Route("api/manga")]
public class MangaController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var manga = new List<Manga>
        {
          new Manga("Berserk", "Reading"),
          new Manga("Vagabond", "PlanToRead")
        };

        return Ok(manga);
    }
}
