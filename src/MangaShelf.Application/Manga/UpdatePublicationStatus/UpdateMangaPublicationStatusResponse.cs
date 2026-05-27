namespace MangaShelf.Application.Manga.UpdatePublicationStatus;

public class UpdateMangaPublicationStatusResponse
{
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Author {get; set;} = string.Empty;
    public string Artist {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public string PublicationStatus {get; set;} = string.Empty;
}