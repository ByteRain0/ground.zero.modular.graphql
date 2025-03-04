using GreenDonut.Data;
using MediatR;

namespace Anime.Contracts.Services.Studio.Queries;

public record GetStudios(
    PagingArguments PagingArguments,
    QueryContext<Contracts.Models.Studio>? QueryContext = default)
    : IRequest<Page<Models.Studio>>;