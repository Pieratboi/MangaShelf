namespace MangaShelf.Application.ChapterReleases.Create;

public class CreateChapterReleaseRequest
{
    public int ScanlatorId {get; set;}
    public string? SourceUrl {get; set;}
    public string? Language {get; set;}
}