namespace Anime.Contracts.Exceptions;

public class AnimeNotFoundException : Exception, IAnimeBasedException
{
    public static string ErrorMessage => "Anime not found";

    public string? IdOrTitle { get; set; }
    
    public AnimeNotFoundException(string idOrTitle) : base(message: ErrorMessage)
    {
        IdOrTitle = idOrTitle;
    }
}