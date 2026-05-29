using MangaShelf.Application.Common.Exceptions;
using MangaShelf.Application.Manga;
using MangaShelf.Domain.Entities;

namespace MangaShelf.Application.Chapters.GetByMangaId;

public class GetChaptersByMangaIdUseCase
{
    private readonly IMangaRepository _mangaRepository;
    private readonly IChapterRepository _chapterRepository;

    public GetChaptersByMangaIdUseCase(IMangaRepository mangaRepository, IChapterRepository chapterRepository)
    {
        _mangaRepository = mangaRepository;
        _chapterRepository = chapterRepository;
    }

    public async Task<List<ChapterResponse>?> ExecuteAsync(int mangaId)
    {
        if(mangaId <= 0)
        {
            throw new ApplicationValidationException("Manga id must be greater than 0.");
        }

        var manga = await _mangaRepository.GetByIdAsync(mangaId);

        if(manga is null)
        {
            return null;
        }

        var chapters = await _chapterRepository.GetByMangaIdAsync(mangaId);

        return chapters.Select(c => new ChapterResponse
        {
            Id = c.Id,
            MangaId = c.MangaId,
            Number = c.Number,
            Title = c.Title
        }).ToList();
    }
}