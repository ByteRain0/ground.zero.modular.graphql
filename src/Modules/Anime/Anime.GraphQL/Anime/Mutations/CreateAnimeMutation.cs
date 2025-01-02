using Anime.Contracts.Services.Anime.Commands;
using Anime.Contracts.Services.Anime.Queries;
using MediatR;

namespace Anime.GraphQL.Anime.Mutations;

[MutationType]
public static class CreateAnimeMutation
{
    public static async Task<Contracts.Models.Anime?> CreateAnimeAsync(
        CreateAnime command,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        
        return await mediator.Send(
            new GetAnimeByTitle(command.Title),
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