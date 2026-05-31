namespace MangaShelf.Application.ChapterPages.GetByRelease;

public class ChapterPageResponse
{
    public int Id {get; set;}
    public int ChapterReleaseId {get; set;}
    public int PageNumber {get; set;}
    public string ImageUrl {get; set;} = string.Empty;
}