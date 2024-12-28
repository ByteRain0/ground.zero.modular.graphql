using Anime.Contracts.Services.Anime.Commands;
using Anime.Contracts.Services.Anime.Queries;
using Core.Clasifiers;
using MediatR;

namespace Anime.GraphQL.Anime.Mutations;

[MutationType]
public static class UpdateAnimeMutation
{
    public static async Task<Contracts.Models.Anime?> UpdateAnimeAsync(
        UpdateAnime command,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        
        return await mediator.Send(
            new GetAnimeById(command.Id),
            cancellationToken);
    }
}

public class UpdateAnimeInputType : InputObjectType<UpdateAnime>
{
    protected override void Configure(IInputObjectTypeDescriptor<UpdateAnime> descriptor)
    {
        descriptor.BindFieldsImplicitly();

        descriptor.Field(x => x.Id)
            .ID<Contracts.Models.Anime>();
        
        descriptor.Field(x => x.Title)
            .DefaultValue(string.Empty);
        
        descriptor.Field(x => x.StudioId)
            .DefaultValue(0);
        
        descriptor.Field(x => x.ReleaseDate)
            .DefaultValue(DateTime.MinValue);
        
        descriptor.Field(x => x.Synopsis)
            .DefaultValue(string.Empty);

        descriptor.Field(x => x.Demographics)
            .DefaultValue(Demographics.Shounen);

        descriptor.Field(x => x.TotalEpisodes)
            .DefaultValue(12);
    }
}