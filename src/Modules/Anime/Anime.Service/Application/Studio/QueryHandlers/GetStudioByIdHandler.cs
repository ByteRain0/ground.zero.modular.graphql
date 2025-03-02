using Anime.Contracts.Services.Studio.Queries;
using Anime.Service.Infrastructure.Data.DataLoaders;
using GreenDonut.Data;
using MediatR;

namespace Anime.Service.Application.Studio.QueryHandlers;

internal class GetStudioByIdHandler(IStudioByIdDataLoader dataLoader)
    : IRequestHandler<GetStudioById, Contracts.Models.Studio?>
{
    public async Task<Contracts.Models.Studio?> Handle(
        GetStudioById request,
        CancellationToken cancellationToken) =>
        await dataLoader
            .With(request.QueryContext)
            .LoadAsync(request.StudioId, cancellationToken);
}