using MangaShelf.Domain.Common.Exceptions;

namespace MangaShelf.Domain.Entities;

public class Chapter
{
    private const int MaxTitleLength = 100;

    public int Id {get; private set;}
    public int MangaId {get; private set;}
    public int Number {get; private set;}
    public string Title {get; private set;} = string.Empty;

    public Manga Manga {get; private set;} = null!;

    private Chapter()
    {
    }

    public Chapter(int mangaId, int number, string? title = null)
    {
        SetMangaId(mangaId);
        SetNumber(number);
        SetTitle(title);
    }

    public Chapter(int id, int mangaId, int number, string? title = null)
    {
        if(id <= 0)
        {
            throw new DomainValidationException("Chapter id must be greater than 0.");
        }

        Id = id;
        SetMangaId(mangaId);
        SetNumber(number);
        SetTitle(title);
    }

    private void SetMangaId(int mangaId)
    {
        if(mangaId <= 0)
        {
            throw new DomainValidationException("Manga id must be greater than 0.");
        }

        MangaId = mangaId;
    }

    private void SetNumber(int number)
    {
        if(number <= 0)
        {
            throw new DomainValidationException("Chapter number must be greater than 0.");
        }

        Number = number;
    }

    private void SetTitle(string? title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Title = string.Empty;
            return;
        }

        if(title.Trim().Length > MaxTitleLength)
        {
            throw new DomainValidationException($"Title length can not be longer than {MaxTitleLength} characters.");
        }

        Title = title.Trim();
    }
}