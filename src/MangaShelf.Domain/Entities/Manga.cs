using System.Numerics;
using MangaShelf.Domain.Enums;
using MangaShelf.Domain.Common.Exceptions;


namespace MangaShelf.Domain.Entities;

public class Manga
{
    private const int MaxDescriptionLength = 2000;
    private const int MaxCreatorNameLength = 100;

    public int Id {get; private set;}
    public string Title {get; private set;} = string.Empty;
    public string Author {get; private set;} = string.Empty;
    public string Artist {get; private set;} = string.Empty;
    public string Description {get; private set;} = string.Empty;
    public PublicationStatus PublicationStatus {get; private set;}

    private Manga()
    {
    }

    public Manga(string title, PublicationStatus publicationStatus, 
    string? author = null, string? artist = null, string? description = null)
    {
        SetTitle(title);
        SetPublicationStatus(publicationStatus);
        SetAuthor(author);
        SetArtist(artist);
        SetDescription(description);
    }
    public Manga(int id, string title, PublicationStatus publicationStatus, 
    string? author = null, string? artist = null, string? description = null)
    {
        if (id <= 0)
        {
            throw new DomainValidationException("Manga id must be greater than zero.");
        }

        Id = id;
        SetTitle(title);
        SetPublicationStatus(publicationStatus);
        SetAuthor(author);
        SetArtist(artist);
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

    private void SetPublicationStatus(PublicationStatus publicationStatus)
    {
        if (!Enum.IsDefined(publicationStatus))
        {
            throw new DomainValidationException("Invalid manga status.");
        }

        PublicationStatus = publicationStatus;
    }

    private void SetAuthor(string? author)
    {
        if (string.IsNullOrWhiteSpace(author))
        {
            Author = string.Empty;
            return;
        }

        if(author.Trim().Length > MaxCreatorNameLength)
        {
            throw new DomainValidationException($"Manga author cannot be longer than {MaxCreatorNameLength} characters.");
        }

        Author = author.Trim();
    }

    private void SetArtist(string? artist)
    {
        if (string.IsNullOrWhiteSpace(artist))
        {
            artist = string.Empty;
            return;
        }

        if(artist.Trim().Length > MaxCreatorNameLength)
        {
            throw new DomainValidationException($"Manga artist cannot be longer than {MaxCreatorNameLength} characters.");
        }

        Artist = artist.Trim();
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

    public void ChangePublicationStatus(PublicationStatus publicationStatus)
    {
        SetPublicationStatus(publicationStatus);
    }

    public void ChangeTitle(string title)
    {
        SetTitle(title);
    }

    public void ChangeAuthor(string? author)
    {
        SetAuthor(author);
    }

    public void ChangeArtist(string? artist)
    {
        SetArtist(artist);
    }

    public void ChangeDescription(string? description)
    {
        SetDescription(description);
    }
}