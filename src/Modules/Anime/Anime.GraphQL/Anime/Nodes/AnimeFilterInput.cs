using Core.QueryFilters;
using HotChocolate.Data.Filters;

namespace Anime.GraphQL.Anime.Nodes;

/// <summary>
/// Define what we can filter animes by.
/// </summary>
public class AnimeFilterInput : FilterInputType<Contracts.Models.Anime>
{
    protected override void Configure(IFilterInputTypeDescriptor<Contracts.Models.Anime> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        
        descriptor.Field(a => a.Title)
            .Type<SearchStringOperationFilterInputType>();

        //TODO: check how I can use relay style ID's here.
        descriptor.Field(x => x.StudioId);

        descriptor.Field(x => x.ReleaseDate);
        
        descriptor.Field(x => x.Demographics);
        
        descriptor.Field(x => x.TotalEpisodes);

        descriptor.Field(x => x.IsCompleted);
        
        descriptor.Field(x => x.IsAiring);
    }
}