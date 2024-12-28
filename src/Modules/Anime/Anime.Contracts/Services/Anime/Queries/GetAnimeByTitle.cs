using FluentValidation;
using MediatR;

namespace Anime.Contracts.Services.Anime.Queries;

public record GetAnimeByTitle(string Title) : IRequest<Contracts.Models.Anime?>;

public class GetAnimeByTitleValidator : AbstractValidator<GetAnimeByTitle>
{
    public GetAnimeByTitleValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty();
    }
}