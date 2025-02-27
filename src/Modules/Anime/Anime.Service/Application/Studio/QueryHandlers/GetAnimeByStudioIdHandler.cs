using Anime.Contracts.Services.Studio.Queries;
using Anime.Service.Infrastructure.Data.DataLoaders;
using GreenDonut.Data;
using MediatR;

namespace Anime.Service.Application.Studio.QueryHandlers;

public class GetAnimeByStudioIdHandler(IAnimeByStudioIdDataLoader dataLoader) 
    : IRequestHandler<GetAnimeByStudioId, Page<Contracts.Models.Anime>>
{
    public async Task<Page<Contracts.Models.Anime>> Handle(
        GetAnimeByStudioId request,
        CancellationToken cancellationToken) =>
        await dataLoader
            .With(
                pagingArguments: request.PagingArguments,
                context: request.QueryContext)
            .LoadAsync(request.StudioId, cancellationToken) ?? Page<Contracts.Models.Anime>.Empty;
}