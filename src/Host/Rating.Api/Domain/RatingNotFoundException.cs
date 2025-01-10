namespace Rating.Api.Domain;

public class RatingNotFoundException : Exception
{
    public RatingNotFoundException(string entityType, int id)
        : base($"Ratings for entity of type {entityType} with provided Id {id} not found")
    {
    }
}