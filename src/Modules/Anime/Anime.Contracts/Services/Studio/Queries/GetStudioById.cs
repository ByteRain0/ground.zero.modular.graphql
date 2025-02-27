using GreenDonut.Data;
using MediatR;

namespace Anime.Contracts.Services.Studio.Queries;

//Uncomment once full auth with KeyCloak is setup.
//[AuthorizeRoles("Administrator")]
public record GetStudioById(
    int StudioId,
    QueryContext<Contracts.Models.Studio> QueryContext)
    : IRequest<Models.Studio?>;