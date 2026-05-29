namespace MangaShelf.Application.ChapterReleases.Update;

public class UpdateChapterReleaseRequest
{
    public int? ScanlatorId {get; set;}
    public string? SourceUrl {get; set;}
    public string? Language {get; set;}
}