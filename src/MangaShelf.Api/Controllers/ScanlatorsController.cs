using MangaShelf.Application.Scanlators.Create;
using MangaShelf.Application.Scanlators.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace MangaShelf.Api.Controllers;

[ApiController]
[Route("api/scanlators")]
public class ScanlatorsController : ControllerBase
{
    private readonly CreateScanlatorUseCase _createScanlatorUseCase;
    private readonly GetAllScanlatorsUseCase _getAllScanlatorsUseCase;

    public ScanlatorsController(
        CreateScanlatorUseCase createScanlatorUseCase,
        GetAllScanlatorsUseCase getAllScanlatorsUseCase)
    {
        _createScanlatorUseCase = createScanlatorUseCase;
        _getAllScanlatorsUseCase = getAllScanlatorsUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var scanlators = await _getAllScanlatorsUseCase.ExecuteAsync();

        return Ok(scanlators);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateScanlatorRequest request)
    {
        var scanlator = await _createScanlatorUseCase.ExecuteAsync(request);

        return CreatedAtAction(
            nameof(GetAll),
            scanlator
        );
    }
}