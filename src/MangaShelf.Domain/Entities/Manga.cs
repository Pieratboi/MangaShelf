using System.Numerics;
using MangaShelf.Domain.Enums;
using MangaShelf.Domain.Common.Exceptions;


namespace MangaShelf.Domain.Entities;

public class Manga
{
    public int Id {get; private set;}
    public string Title {get; private set;} = string.Empty;
    public MangaStatus Status {get; private set;}

    private Manga()
    {
    }

    public Manga(string title, MangaStatus status)
    {
        SetTitle(title);
        SetStatus(status);
    }
    public Manga(int id, string title, MangaStatus status)
    {
        if (id <= 0)
        {
            throw new DomainValidationException("Manga id must be greater than zero.");
        }

        Id = id;
        SetTitle(title);
        SetStatus(status);
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainValidationException("Manga title is required.");
        }

        Title = title.Trim();
    }

    private void SetStatus(MangaStatus status)
    {
        if (!Enum.IsDefined(status))
        {
            throw new DomainValidationException("Invalid manga status.");
        }

        Status = status;
    }

    public void ChangeStatus(MangaStatus status)
    {
        SetStatus(status);
    }
}