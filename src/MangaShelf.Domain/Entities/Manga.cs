using System.Numerics;
using MangaShelf.Domain.Enums;
using MangaShelf.Domain.Common.Exceptions;


namespace MangaShelf.Domain.Entities;

public class Manga
{
    private const int MaxDescriptionLength = 2000;

    public int Id {get; private set;}
    public string Title {get; private set;} = string.Empty;
    public string Description {get; private set;} = string.Empty;
    public MangaStatus Status {get; private set;}

    private Manga()
    {
    }

    public Manga(string title, MangaStatus status, string? description = null)
    {
        SetTitle(title);
        SetStatus(status);
        SetDescription(description);
    }
    public Manga(int id, string title, MangaStatus status, string? description = null)
    {
        if (id <= 0)
        {
            throw new DomainValidationException("Manga id must be greater than zero.");
        }

        Id = id;
        SetTitle(title);
        SetStatus(status);
        SetDescription(description);
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

    private void SetDescription(string? description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            Description = string.Empty;
            return;
        }

        var trimmedDescription = description.Trim();

        if(trimmedDescription.Length > MaxDescriptionLength)
        {
            throw new DomainValidationException($"Manga description cannot be larger than {MaxDescriptionLength} characters.");
        }

        Description = trimmedDescription;
    }

    public void ChangeStatus(MangaStatus status)
    {
        SetStatus(status);
    }
}