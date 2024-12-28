using MediatR;

namespace Anime.Contracts.Services.Studio.Queries;

//Uncomment once full auth with KeyCloack is setup.
//[AuthorizeRoles("Administrator")]
public record GetStudioById(int StudioId) 
    : IRequest<Models.Studio?>;