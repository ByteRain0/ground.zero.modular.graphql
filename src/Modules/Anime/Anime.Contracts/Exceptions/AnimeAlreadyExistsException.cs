namespace Anime.Contracts.Exceptions;

public class AnimeAlreadyExistsException : Exception, IAnimeBasedException
{
    public static string ErrorMessage => "Anime already exists.";
    
    public string? IdOrTitle { get; set; }
    
    public AnimeAlreadyExistsException(string idOrTitle) : base(message: ErrorMessage)
    {
        IdOrTitle = idOrTitle;
    }
}