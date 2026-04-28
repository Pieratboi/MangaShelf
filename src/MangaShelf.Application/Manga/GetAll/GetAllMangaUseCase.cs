using System.Data.Common;
using System.Reflection;
using MangaShelf.Domain.Entities;

namespace MangaShelf.Application.Manga.GetAll;

public class GetAllMangaUseCase
{
    public List<MangaResponse> Execute()
    {
        var manga = new List<Domain.Entities.Manga>
        {
            new("Berserk","Reading"),
            new("Vagabond", "PlanToRead")
        };

        return manga.Select(m => new MangaResponse
        {
            Id = m.Id,
            Title = m.Title,
            Status = m.Status
        }).ToList();
    }
}