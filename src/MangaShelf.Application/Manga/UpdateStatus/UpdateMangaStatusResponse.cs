namespace MangaShelf.Application.Manga.UpdateStatus;

public class UpdateMangaStatusResponse
{
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public string Status {get; set;} = string.Empty;
}