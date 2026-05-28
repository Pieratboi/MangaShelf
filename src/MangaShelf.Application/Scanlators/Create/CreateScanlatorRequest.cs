namespace MangaShelf.Application.Scanlators.Create;

public class CreateScanlatorRequest
{
    public string Name {get; set;} = string.Empty;
    public string? WebsiteUrl {get; set;}
}