namespace MangaShelf.Application.ChapterReleases.GetById;

public class ChapterReleaseDetailsResponse
{
    public int Id {get; set;}
    public int ChapterId {get; set;}
    public int ScanlatorId {get; set;}
    public string ScanlatorName {get; set;} = string.Empty;
    public string SourceUrl {get; set;} = string.Empty;
    public string Language {get; set;} = string.Empty;
}