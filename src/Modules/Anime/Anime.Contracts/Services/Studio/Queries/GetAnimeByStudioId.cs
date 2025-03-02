using GreenDonut.Data;
using MediatR;

namespace Anime.Contracts.Services.Studio.Queries;

public record GetAnimeByStudioId(
    int StudioId,
    PagingArguments PagingArguments,
    QueryContext<Contracts.Models.Anime>? QueryContext)
    : IRequest<Page<Contracts.Models.Anime>>;