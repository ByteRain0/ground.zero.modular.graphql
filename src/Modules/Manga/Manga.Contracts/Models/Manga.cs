using Core.Clasifiers;

namespace Manga.Contracts.Models;

public class Manga
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    
    public DateTime ReleaseDate { get; set; }

    public string Synopsis { get; set; } = string.Empty;
    
    public Demographics Demographics { get; set; }
    
    public int TotalChapters { get; set; }
    
    public bool IsCompleted { get; set; } 
    
    public bool IsAiring { get; set; }

    public int AuthorId { get; set; }
    
    public Author Author { get; set; }
}