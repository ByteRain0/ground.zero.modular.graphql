# 0017. Data Loaders

**Date:** 2025-01-11

## Problem

API clients may need to fetch additional, related information about entities such as studios or authors linked to anime/manga. </br>
The challenge is that the specific data clients require can vary, and their requests might not be predictable in advance. </br>
Ideally, all required data should be retrievable in a single API query. </br>

---

## Decision

We will use **data loaders** to address the **N+1 problem** that can arise when GraphQL field resolvers independently retrieve related data. </br>
To enable this, additional fields will be added to the model nodes, which will resolve their data by making efficient batch calls via data loaders. </br>

---

## Consequences

### Challenges with Default GraphQL Field Resolvers

1. **Atomic Nature of Resolvers:** </br>
   Field resolvers operate independently and are unaware of the overall query, leading to inefficiencies when multiple resolvers fetch similar or identical data. </br>

2. **N+1 Problem:** </br>
   For example, if a query retrieves 10 anime and their respective studios, without optimization, the process would involve: </br>
    - 1 `GetAnimeQuery` to fetch 10 anime. </br>
    - 10 individual `GetStudioById` queries to fetch studios, resulting in 11 total queries instead of batching them into fewer queries. </br>

---

### Data Loader Architecture

To prevent the N+1 problem, we will implement **data loaders** with the following approach:

1. **Query Example:** </br>
    - The API exposes the `GetAnime` query. </br>
    - The `AnimeNode` is structured as follows:
        - **Ignore the `StudioId` field** in the `Anime` model.
        - Add a **custom field resolver**:
          ```csharp
          public static async Task<Studio?> GetStudioAsync([Parent] Anime anime, IMediator mediator) 
              => await mediator.Send(new GetStudioById(anime.StudioId));
          ```

2. **Resolver Workflow:** </br>
    - A GraphQL request to fetch 10 anime with their respective studios triggers the resolver for `GetStudioAsync`. </br>
    - The resolver sends a `GetStudioById` **MediatR request** for each anime. </br>
    - The `GetStudioById` handler calls the **data loader** to fetch the required studio. </br>
    - The **data loader** in turn caches the request to fetch the studio and executes it only after all resolvers from the request are executed.

3. **Batching and Caching:** </br>
    - Data loaders cache promises for fetching data, ensuring that the same key is resolved only once per request scope. </br>
    - Once all resolvers are executed, the data loader triggers a **batch fetch** to retrieve data in a single operation. </br>

4. **Data Loader Setup:** </br>
    - Data loaders will be defined in the **Infrastructure folder** of the respective service's class libraries. </br>
    - Methods annotated with `[DataLoader]` generate auto-implemented interfaces via the **GreenDonut library**. </br>
    - Example setup in `ModuleInfo` ensures correct scoping: </br>
      ```csharp
      [assembly: DataLoaderDefaults(ServiceScope = DataLoaderServiceScope.DataLoaderScope)]
      ```
    - Data loader classes are defined as `internal sealed`. </br>
    - Inject data loader services from the **DI container** to promote reuse and efficiency. </br>

---

### Benefits

1. Reduces the number of database queries by batching similar requests into fewer operations. </br>
2. Improves performance and scalability for GraphQL queries involving related data. </br>
3. Simplifies client-side queries by allowing all required data to be retrieved in a single API request. </br>

---

### Additional Resources

- [HotChocolate DataLoader Documentation](https://chillicream.com/docs/hotchocolate/v13/fetching-data/dataloader) </br> 
