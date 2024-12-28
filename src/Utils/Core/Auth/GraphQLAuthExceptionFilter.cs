using Core.Extensions;
using HotChocolate;

namespace Core.Auth;

/// <summary>
/// Note: Supports DI.
/// Can be expanded with additional data from Context.
/// </summary>
public class GraphQLAuthExceptionFilter : IErrorFilter
{ 
    public IError OnError(IError error)
    {
        if (error.Exception is ForbiddenException forbiddenException)
        {
            error.WithMessage(forbiddenException.Message)
                .WithCode("FORBIDDEN");

            return ErrorBuilder.FromError(error)
                .SetTraceIdentifiers()
                .Build();
        }

        return error;
    }
}