namespace MangaShelf.Application.Manga.Create;

public class CreateMangaResponse
{
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Author {get; set;} = string.Empty;
    public string Artist {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public string Status {get; set;} = string.Empty;
}