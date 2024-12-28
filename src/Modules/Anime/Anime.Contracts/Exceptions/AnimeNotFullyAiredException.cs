namespace Anime.Contracts.Exceptions;

public class AnimeNotFullyAiredException : Exception, IAnimeBasedException
{
    public static string ErrorMessage => "Anime not fully aired.";

    public string? IdOrTitle { get; set; }
    
    public AnimeNotFullyAiredException(string idOrTitle) : base(message: ErrorMessage)
    {
        IdOrTitle = idOrTitle;
    }
}