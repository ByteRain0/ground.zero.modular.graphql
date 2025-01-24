using MediatR;
using Rating.Api.Application.Rating.GetRatingByIdQuery;

namespace Rating.Api.GraphQL.Nodes;

[ObjectType<Domain.Anime>]
public static partial class AnimeNode
{
    static partial void Configure(IObjectTypeDescriptor<Domain.Anime> descriptor)
    {
        descriptor.BindFieldsImplicitly();
    }
    
    public static async Task<double?> GetTotalRating(
        [Parent] Domain.Anime parent,
        IMediator mediator, 
        CancellationToken cancellationToken) 
        => await mediator.Send(new GetRatingById(parent.Id, "Anime"), cancellationToken);
}