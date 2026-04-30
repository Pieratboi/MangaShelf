namespace MangaShelf.Application.Manga.GetById;

public class MangaDetailsResponse
{
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Status {get; set;} = string.Empty;
}