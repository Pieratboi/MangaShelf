using MangaShelf.Domain.Common.Exceptions;

namespace MangaShelf.Domain.Entities;

public class ChapterPage
{
    private const int MaxImageUrlLength = 1000;

    public int Id {get; private set;}
    public int ChapterReleaseId {get; private set;}
    public int PageNumber {get; private set;}
    public string ImageUrl {get; private set;} = string.Empty;

    public ChapterRelease ChapterRelease {get; private set;} = null!;

    private ChapterPage()
    {
    }

    public ChapterPage(int chapterReleaseId, int pageNumber, string imageUrl)
    {
        SetChapterReleaseId(chapterReleaseId);
        SetPageNumber(pageNumber);
        SetImageUrl(imageUrl);
    }

    public ChapterPage(int id, int chapterReleaseId, int pageNumber, string imageUrl)
    {
        Id = id;
        SetChapterReleaseId(chapterReleaseId);
        SetPageNumber(pageNumber);
        SetImageUrl(imageUrl);
    }

    private void SetChapterReleaseId(int chapterReleaseId)
    {
        if(chapterReleaseId <= 0)
        {
            throw new DomainValidationException("Chapter release id must be greater than 0.");
        }

        ChapterReleaseId = chapterReleaseId;
    }

    private void SetPageNumber(int pageNumber)
    {
        if(pageNumber <= 0)
        {
            throw new DomainValidationException("Page number must be greater than 0.");
        }

        PageNumber = pageNumber;
    }

    private void SetImageUrl(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
        {
            throw new DomainValidationException("Page image URL is required.");
        }

        if(imageUrl.Trim().Length > MaxImageUrlLength)
        {
            throw new DomainValidationException($"Page image URl cannot be longer than {MaxImageUrlLength} characters.");
        }

        ImageUrl = imageUrl.Trim();
    }

    public void ChangePageNumber(int pageNumber)
    {
        SetPageNumber(pageNumber);
    }

    public void ChangeImageUrl(string imageUrl)
    {
        SetImageUrl(imageUrl);
    }
}