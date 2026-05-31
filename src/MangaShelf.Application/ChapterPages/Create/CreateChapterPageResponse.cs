namespace MangaShelf.Application.ChapterPages.Create;

public class CreateChapterPageResponse
{
    public int Id {get; set;}
    public int ChapterReleaseId {get; set;}
    public int PageNumber {get; set;}
    public string ImageUrl {get; set;} = string.Empty;
}