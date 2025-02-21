using GreenDonut.Data;
using MediatR;

namespace Anime.Contracts.Services.Studio.Queries;

public record GetStudios(PagingArguments PagingArguments) 
    : IRequest<Page<Models.Studio>>;