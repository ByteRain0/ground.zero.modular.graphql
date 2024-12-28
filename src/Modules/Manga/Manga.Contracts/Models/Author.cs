namespace Manga.Contracts.Models;

public class Author
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Manga> Mangas { get; set; }
}