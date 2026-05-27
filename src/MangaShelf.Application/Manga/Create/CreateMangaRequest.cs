namespace MangaShelf.Application.Manga.Create;

public class CreateMangaRequest
{
    public string Title {get; set;} = string.Empty;

    public string? Author {get; set;}
    public string? Artist {get; set;}
    public string? Description {get; set;}
    public string PublicationStatus {get; set;} = string.Empty;
}