using Anime.Contracts.Services.Studio.Queries;
using GreenDonut.Data;
using MediatR;

namespace Anime.GraphQL.Anime.Nodes;

/// <summary>
/// Re-write the graph node / model that is exposed to the client application.
/// </summary>
[ObjectType<Contracts.Models.Anime>]
public static partial class AnimeNode
{
    static partial void Configure(IObjectTypeDescriptor<Contracts.Models.Anime> descriptor)
    {
        descriptor.BindFieldsImplicitly();

        descriptor.Field(x => x.StudioId).Ignore();
    }

    public static int InternalId([Parent] Contracts.Models.Anime anime) => anime.Id;

    /// <summary>
    /// Since we are not interested in studio by default
    /// we are going to load it if necessary via data loader.
    /// Return type is nullable in case the studio handler fails
    /// we can at least return a partial result.
    /// </summary>
    public static async Task<Contracts.Models.Studio?> GetStudioAsync(
        // Force load the StudioId into the data loader while still hiding it from the Client.
        [Parent(requires: nameof(Contracts.Models.Anime.StudioId))]
        Contracts.Models.Anime anime,
        QueryContext<Contracts.Models.Studio> queryContext,
        IMediator mediator) =>
        await mediator.Send(new GetStudioById(anime.StudioId, queryContext));
}