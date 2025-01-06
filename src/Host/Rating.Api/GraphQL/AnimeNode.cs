using MediatR;
using Rating.Api.Application.Rating.GetRatingByIdQuery;
using Rating.Api.Models;

namespace Rating.Api.GraphQL;

[ObjectType<Anime>]
public static partial class AnimeNode
{
    static partial void Configure(IObjectTypeDescriptor<Anime> descriptor)
    {
        descriptor.BindFieldsImplicitly();
    }

    public static async Task<double> GetTotalRating([Parent] Manga parent,
        IMediator mediator, 
        CancellationToken cancellationToken) 
        => await mediator.Send(new GetRatingById(parent.Id, "Anime"), cancellationToken);
}