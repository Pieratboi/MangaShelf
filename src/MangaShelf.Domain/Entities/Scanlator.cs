using MangaShelf.Domain.Common.Exceptions;

namespace MangaShelf.Domain.Entities;

public class Scanlator
{
    private const int MaxNameLength = 100;
    private const int MaxWebsiteUrlLength = 400;

    public int Id {get; private set;}
    public string Name {get; private set;} = string.Empty;
    public string WebsiteUrl {get; private set;} = string.Empty;

    private Scanlator()
    {
    }

    public Scanlator(string name, string? websiteUrl = null)
    {
        SetName(name);
        SetWebsiteUrl(websiteUrl);
    }

    public Scanlator(int id, string name, string? websiteUrl = null)
    {
        if(id <= 0)
        {
            throw new DomainValidationException("Scanlator id must be greater than 0.");
        }

        Id = id;
        SetName(name);
        SetWebsiteUrl(websiteUrl);
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainValidationException("Scanlator name is required.");    
        }

        if(name.Trim().Length > MaxNameLength)
        {
            throw new DomainValidationException($"Scanlator name cannot be longer than {MaxNameLength} characters.");
        }

        Name = name.Trim();
    }

    private void SetWebsiteUrl(string? websiteUrl)
    {
        if (string.IsNullOrWhiteSpace(websiteUrl))
        {
            WebsiteUrl = string.Empty;
            return;
        }

        if(websiteUrl.Trim().Length > MaxWebsiteUrlLength)
        {
            throw new DomainValidationException($"Scanlator website URL cannot be longer than {MaxWebsiteUrlLength} characters.");
        }

        WebsiteUrl = websiteUrl.Trim();
    }
}