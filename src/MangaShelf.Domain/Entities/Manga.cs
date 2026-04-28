using System.Numerics;

namespace MangaShelf.Domain.Entities;

public class Manga
{
    public int Id {get; private set;}
    public string Title {get; private set;}
    public string Status {get; private set;}

    public Manga(int id, string title, string status)
    {
        Id = id;
        Title = title;
        Status = status;
    }
}