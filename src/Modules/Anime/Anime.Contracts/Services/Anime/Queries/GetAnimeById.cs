using FluentValidation;
using MediatR;

namespace Anime.Contracts.Services.Anime.Queries;

public record GetAnimeById(int Id) : IRequest<Models.Anime?>;
    
public class GetAnimeByIdValidator : AbstractValidator<GetAnimeById>
{
    public GetAnimeByIdValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}