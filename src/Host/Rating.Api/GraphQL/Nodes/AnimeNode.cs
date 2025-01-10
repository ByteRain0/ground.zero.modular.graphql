using MediatR;
using Rating.Api.Application.Rating.GetRatingByIdQuery;
using Rating.Api.Domain;

namespace Rating.Api.GraphQL.Nodes;

[ObjectType<Anime>]
public static partial class AnimeNode
{
    static partial void Configure(IObjectTypeDescriptor<Anime> descriptor)
    {
        descriptor.BindFieldsImplicitly();
    }
    
    public static async Task<double?> GetTotalRating(
        [Parent] Anime parent,
        IMediator mediator, 
        CancellationToken cancellationToken) 
        => await mediator.Send(new GetRatingById(parent.Id, "Anime"), cancellationToken);
}