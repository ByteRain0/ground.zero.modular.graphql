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
            return ErrorBuilder.FromError(error)
                .SetCode("FORBIDDEN")
                .SetMessage(forbiddenException.Message)
                .SetTraceIdentifiers()
                .Build();
        }

        return error;
    }
}