namespace MangaShelf.Application.ChapterReleases.Update;

public class UpdateChapterReleaseResponse
{
    public int Id {get; set;}
    public int ChapterId {get; set;}
    public int ScanlatorId {get; set;}
    public string ScanlatorName {get; set;} = string.Empty;
    public string SourceUrl {get; set;} = string.Empty;
    public string Language {get; set;} = string.Empty;
}