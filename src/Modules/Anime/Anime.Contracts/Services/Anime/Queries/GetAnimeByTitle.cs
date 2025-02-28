using GreenDonut.Data;
using MediatR;

namespace Anime.Contracts.Services.Anime.Queries;

public record GetAnimeByTitle(
    string Title) 
    : IRequest<Contracts.Models.Anime?>;