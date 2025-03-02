using GreenDonut.Data;
using MediatR;

namespace Anime.Contracts.Services.Anime.Queries;

public record GetAnime(
    PagingArguments PagingArguments,
    QueryContext<Contracts.Models.Anime>? QueryContext) 
    : IRequest<Page<Contracts.Models.Anime>>;