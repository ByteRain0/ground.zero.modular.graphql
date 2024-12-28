namespace Anime.Contracts.Models;

public class Studio
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public IEnumerable<Anime> Animes { get; set; } = new List<Anime>();
}