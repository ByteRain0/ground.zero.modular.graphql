using GreenDonut.Data;
using MediatR;

namespace Anime.Contracts.Services.Anime.Queries;

public record GetAnimeByTitle(
    string Title,
    QueryContext<Models.Anime> QueryContext) 
    : IRequest<Contracts.Models.Anime?>;