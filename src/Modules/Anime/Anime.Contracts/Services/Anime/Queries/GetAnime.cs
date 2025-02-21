using FluentValidation;
using GreenDonut.Data;
using MediatR;

namespace Anime.Contracts.Services.Anime.Queries;

public record GetAnime(
    PagingArguments PagingArguments, 
    GetAnimeQueryFilters QueryFilters) 
    : IRequest<Page<Contracts.Models.Anime>>;

public class GetAnimeQueryFilters
{
    public int? StudioId { get; set; }

    public bool? IsCompleted { get; set; }
    
    public bool? IsAiring { get; set; }

    public string? Title { get; set; }
}

public class GetAnimeQueryValidator : AbstractValidator<GetAnime>
{
    public GetAnimeQueryValidator()
    {
        //
    }
}