# 7. Error Handling Strategy

Date: 2025-02-22  

## Status

Accepted

## Context

To ensure a unified and consistent error-handling approach across the system, we need a well-defined strategy that classifies errors appropriately and standardizes their representation in API responses.

## Decision

### Technical Errors

Technical errors are unexpected issues that occur at runtime, such as null responses on non-null expected fields or cross-cutting failures. These errors will be included in the `errors` field of the API response. Every response may contain one or more technical errors.

Example response:

```json
{
  "errors": [
    {
      "message": "Unexpected Execution Error",
      "locations": [
        {
          "line": 2,
          "column": 3
        }
      ],
      "path": [
        "createAnime"
      ],
      "extensions": {
        "message": "User is not authenticated.",
        "code": "FORBIDDEN",
        "traceId": "33b04027c51aac500173cf5d0440e2a9",
        "spanId": "6e3341035b5ef830",
        "stacktrace": "..."
      }
    }
  ],
  "data": null
}
```

**Note:** Consumers may still process partially successful operations, meaning that an incomplete response may contain both `data` and `errors`.

For this project, we categorize general cross-cutting exceptions as technical errors. These include, but are not limited to:

- Validation failures
- Authentication issues
- Authorization failures
- Database errors
- Network/API failures

Each technical exception should have a dedicated `ErrorFilter` to transform the exception into a structured GraphQL error. Example:

```csharp
public class GraphQLAuthExceptionFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        // Catch the expected error, otherwise pass it along the pipeline
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
```

### Domain Errors

Domain errors represent expected exceptions that occur under specific business conditions. Each module will define the exceptions it can produce within the `Contracts` class library. Corresponding `ErrorFilters` will be implemented in the `Service` class library.

To improve API consumption and predictability, domain errors will be explicitly defined in the GraphQL schema.

**Notes:**
- Domain errors primarily occur in **Mutations**.
- **Queries** tend to return nullable responses, reducing the likelihood of domain errors.

To indicate that a mutation can return a specific domain error, we use the `Error<SpecificException>` attribute:

```csharp
[MutationType]
public static class CreateAnimeMutation
{
    [Error<AnimeNotFoundException>]
    public static async Task<Contracts.Models.Anime?> CreateAnimeAsync(
        CreateAnime command,
        CancellationToken cancellationToken,
        IMediator mediator)
    {
        await mediator.Send(command, cancellationToken);
        return await mediator.Send(
            new GetAnimeByTitle(command.Title, default),
            cancellationToken);
    }
}
```

This adds the error type to the possible API responses for the mutation. A GraphQL request example:

```graphql
mutation CreateAnimeMutation($input: CreateAnimeInput!) {
  createAnime(command: $input) {
    anime {
      id
    }
    errors {
      ... on AnimeNotFoundError {
        idOrTitle
        message
      }
    }
  }
}
```

## Consequences

- Consumers can effectively handle both expected (domain) and unexpected (technical) errors.
- API integration becomes more predictable, as consumers are aware of potential failure scenarios and can handle them appropriately.

## Additional Resources

- [Guide to GraphQL Errors](https://productionreadygraphql.com/2020-08-01-guide-to-graphql-errors)
- [Handling Non-Null Fields in GraphQL](https://chillicream.com/docs/hotchocolate/v15/defining-a-schema/non-null)
- [GraphQL Error Handling Overview](https://www.youtube.com/watch?v=Zx0nvTUfjn4)