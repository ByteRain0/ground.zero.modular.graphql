using FluentValidation;
using MediatR;

namespace Anime.Contracts.Services.Anime.Commands;

public record DeleteAnime(int Id) : IRequest;

public class DeleteAnimeValidator : AbstractValidator<DeleteAnime>
{
    public DeleteAnimeValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}