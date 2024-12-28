using Core.Clasifiers;
using FluentValidation;
using HotChocolate;
using MediatR;

namespace Anime.Contracts.Services.Anime.Commands;

public record UpdateAnime(
    int Id,
    Optional<string> Title,
    Optional<int> StudioId,
    Optional<DateTime> ReleaseDate,
    Optional<string> Synopsis,
    Optional<Demographics> Demographics,
    Optional<int> TotalEpisodes) : IRequest;

public class UpdateAnimeValidator : AbstractValidator<UpdateAnime>
{
    public UpdateAnimeValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}