using Core.Clasifiers;

namespace Anime.Contracts.Models;

public class Anime
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    
    public int StudioId { get; set; }
    
    public Studio? Studio { get; set; }
    
    public DateTime ReleaseDate { get; set; }

    public string Synopsis { get; set; } = string.Empty;
    
    public Demographics Demographics { get; set; }
    
    public int TotalEpisodes { get; set; }
    
    public bool IsCompleted { get; set; } 
    
    public bool IsAiring { get; set; }
}