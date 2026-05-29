namespace MangaShelf.Application.Chapters.GetByMangaId;

public class ChapterResponse
{
    public int Id {get; set;}
    public int MangaId {get; set;}
    public int Number {get; set;}
    public string Title {get; set;} = string.Empty;
}