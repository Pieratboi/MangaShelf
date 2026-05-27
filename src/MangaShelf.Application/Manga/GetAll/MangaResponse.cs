namespace MangaShelf.Application.Manga.GetAll;

public class MangaResponse
{
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Author {get; set;} = string.Empty;
    public string Artist {get; set;} = string.Empty;
    public string Status {get; set;} = string.Empty;
}