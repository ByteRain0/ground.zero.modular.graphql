using Core.Auth;
using Core.Clasifiers;
using FluentValidation;
using MediatR;

namespace Anime.Contracts.Services.Anime.Commands;

/// <summary>
/// Add dynamic rules based validation via RulesEngine.
/// Integrate maybe with an Elsa workflow to trigger some notifications in the future.
/// </summary>
/// <param name="Title"></param>
/// <param name="StudioId"></param>
/// <param name="ReleaseDate"></param>
/// <param name="Synopsis"></param>
/// <param name="Demographics"></param>
/// <param name="TotalEpisodes"></param>
//[AuthorizeRoles("app-admin")]
public record CreateAnime(
    string Title,
    int StudioId,
    DateTime ReleaseDate,
    string Synopsis,
    Demographics Demographics,
    int TotalEpisodes) : IRequest;

public class CreateAnimeValidator : AbstractValidator<CreateAnime>
{
    public CreateAnimeValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty();

        RuleFor(x => x.Synopsis)
            .NotEmpty()
            .MaximumLength(10);

        RuleFor(x => x.StudioId)
            .GreaterThan(0);

        RuleFor(x => x.ReleaseDate)
            .NotEmpty();

        RuleFor(x => x.TotalEpisodes)
            .GreaterThan(0);
    }
}