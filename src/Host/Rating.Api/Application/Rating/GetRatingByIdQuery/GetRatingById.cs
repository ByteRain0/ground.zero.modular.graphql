using MediatR;

namespace Rating.Api.Application.Rating.GetRatingByIdQuery;

public record GetRatingById(
    int Id,
    string EntityType) : IRequest<double>;