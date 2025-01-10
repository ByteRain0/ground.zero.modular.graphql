using MediatR;
using Rating.Api.Application.Rating.GetRatingByIdQuery;
using Rating.Api.Domain;

namespace Rating.Api.GraphQL.Nodes;

[ObjectType<Manga>]
public static partial class MangaNode
{
    static partial void Configure(IObjectTypeDescriptor<Manga> descriptor)
    {
        descriptor.BindFieldsImplicitly();
    }

    public static async Task<double?> GetTotalRating(
        [Parent] Manga parent,
        IMediator mediator,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetRatingById(parent.Id, "Manga"), cancellationToken);
}