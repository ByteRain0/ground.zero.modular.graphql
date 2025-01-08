namespace Rating.Api.Domain;

public class Rating
{
    public Guid Id { get; set; }

    public double Mark { get; set; }

    public string EntityType { get; set; } = string.Empty;

    public int EntityId { get; set; }
}