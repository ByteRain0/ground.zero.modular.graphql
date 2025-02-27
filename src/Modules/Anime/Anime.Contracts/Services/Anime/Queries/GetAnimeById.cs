using GreenDonut.Data;
using MediatR;

namespace Anime.Contracts.Services.Anime.Queries;

public record GetAnimeById(
    int Id,
    QueryContext<Models.Anime>? QueryContext)
    : IRequest<Models.Anime?>;