using GreenDonut.Data;
using MediatR;

namespace Anime.Contracts.Services.Studio.Queries;

public record GetStudios(
    PagingArguments PagingArguments,
    QueryContext<Contracts.Models.Studio> QueryContext) 
    : IRequest<Page<Models.Studio>>;