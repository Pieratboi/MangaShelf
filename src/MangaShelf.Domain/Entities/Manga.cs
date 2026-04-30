using System.Numerics;
using MangaShelf.Domain.Enums;
using MangaShelf.Domain.Common.Exceptions;


namespace MangaShelf.Domain.Entities;

public class Manga
{
    public int Id {get; private set;}
    public string Title {get; private set;}
    public MangaStatus Status {get; private set;}

    public Manga(int id, string title, MangaStatus status)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainValidationException("Manga title is required.");
        }
        if (!Enum.IsDefined(status))
        {
            throw new DomainValidationException("Invalid manga status.");
        }

        Id = id;
        Title = title;
        Status = status;
    }
}