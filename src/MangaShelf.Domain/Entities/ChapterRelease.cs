using MangaShelf.Domain.Common.Exceptions;

namespace MangaShelf.Domain.Entities;

public class ChapterRelease
{
    private const int MaxSourceUrlLength = 1000;
    private const int MaxLanguageLength = 20;

    public int Id {get; private set;}
    public int ChapterId {get; private set;}
    public int ScanlatorId {get; private set;}
    public string SourceUrl {get; private set;} = string.Empty;
    public string Language {get; private set;} = "en";

    public Chapter Chapter {get; private set;} = null!;
    public Scanlator Scanlator {get; private set;} = null!;

    private ChapterRelease()
    {
    }

    public ChapterRelease(
        int chapterId,
        int scanlatorId,
        string? sourceUrl = null,
        string? language = null)
    {
        SetChapterId(chapterId);
        SetScanlatorId(scanlatorId);
        SetSourceUrl(sourceUrl);
        SetLanguage(language);
    }

    private void SetChapterId(int chapterId)
    {
        if(chapterId <= 0)
        {
            throw new DomainValidationException("Chapter id must be greater than 0.");
        }

        ChapterId = chapterId;
    }

    private void SetScanlatorId(int scanlatorId)
    {
        if(scanlatorId <= 0)
        {
            throw new DomainValidationException("Scanlator id must be greater than 0.");
        }

        ScanlatorId = scanlatorId;
    }

    private void SetSourceUrl(string? sourceUrl)
    {
        if (string.IsNullOrWhiteSpace(sourceUrl))
        {
            SourceUrl = string.Empty;
            return;
        }

        if(sourceUrl.Trim().Length > MaxSourceUrlLength)
        {
            throw new DomainValidationException($"Chapter release source URL cannot be longer than {MaxSourceUrlLength} characters.");
        }

        SourceUrl = sourceUrl.Trim();
    }

    private void SetLanguage(string? language)
    {
        if (string.IsNullOrWhiteSpace(language))
        {
            Language = "en";
            return;
        }

        if(language.Trim().Length > MaxLanguageLength)
        {
            throw new DomainValidationException($"Chapter release language cannot be longer than {MaxLanguageLength} characters.");
        }

        Language = language.Trim();
    }

    public void ChangeScanlatorId(int scanlatorId)
    {
        SetScanlatorId(scanlatorId);
    }

    public void ChangeSourceUrl(string? sourceUrl)
    {
        SetSourceUrl(sourceUrl);
    }

    public void ChangeLanguage(string? language)
    {
        SetLanguage(language);
    }
}