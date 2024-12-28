namespace Anime.Contracts.Exceptions;

public class AnimeNotAiredException : Exception, IAnimeBasedException
{
    public static string ErrorMessage => "Anime has not yet aired.";
    
    public string? IdOrTitle { get; set; }
    
    public AnimeNotAiredException(string idOrTitle) : base(message: ErrorMessage)
    {
        IdOrTitle = idOrTitle;
    }
}