using Anime.Contracts.Services.Anime.Commands;
using Anime.Contracts.Services.Anime.Queries;
using MediatR;

namespace Anime.GraphQL.Anime.Mutations;

[MutationType]
public static class CreateAnimeMutation
{
    public static async Task<Contracts.Models.Anime?> CreateAnimeAsync(
        CreateAnime command,
        CancellationToken cancellationToken,
        IMediator mediator)
    {
        await mediator.Send(command, cancellationToken);

        // Since there is no selection set on mutation pass default one.
        return await mediator.Send(
            new GetAnimeByTitle(command.Title, default),
            cancellationToken);
    }
}

public class CreateAnimeInputType : InputObjectType<CreateAnime>
{
    protected override void Configure(IInputObjectTypeDescriptor<CreateAnime> descriptor)
    {
        descriptor.BindFieldsImplicitly();

        descriptor.Field(x => x.StudioId)
            .ID<Contracts.Models.Studio>();
    }
}