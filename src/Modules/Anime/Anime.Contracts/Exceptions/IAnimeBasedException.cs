namespace Anime.Contracts.Exceptions;

public interface IAnimeBasedException
{
    public string? IdOrTitle { get; set; }
}