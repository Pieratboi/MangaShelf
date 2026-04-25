using Microsoft.AspNetCore.Mvc;

namespace MangaShelf.Api.Controllers;

[ApiController]
[Route("api/manga")]
public class MangaController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var manga = new[]
        {
          new { Id = 1, Title = "Berserk", Status = "Reading" },
          new {Id = 2, Title = "Vagabond", Status = "PlanToRead" }  
        };

        return Ok(manga);
    }
}
