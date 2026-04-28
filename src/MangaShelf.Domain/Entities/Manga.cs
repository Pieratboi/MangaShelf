using System.Numerics;
using MangaShelf.Domain.Enums;


namespace MangaShelf.Domain.Entities;

public class Manga
{
    public int Id {get; private set;}
    public string Title {get; private set;}
    public MangaStatus Status {get; private set;}

    public Manga(int id, string title, MangaStatus status)
    {
        Id = id;
        Title = title;
        Status = status;
    }
}