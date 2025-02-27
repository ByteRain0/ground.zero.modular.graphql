# 15. Graphql fusion gateway

Date: 2025-02-22

## Status

Accepted

## Context

Users searching for a new anime or manga to watch/read want to see ratings for the anime/manga. </br>

## Decision

A new **Rating.Api** will be created to encapsulate the logic related to ratings. </br>  
To simplify integration for front-end and other consumers, a **Fusion Gateway** will be placed in front of the **Japanese** and **Rating.Api**. </br>

## Consequences

1. The **Rating.Api** will manage all rating-related logic for anime and manga. </br>
2. The initial implementation will utilize an in-memory database seeded with ratings via migrations. </br>
3. A **Fusion Gateway** built using Fusion and Aspire will enable a distributed GraphQL schema. </br>
4. **Rating.Api** will introduce an internal `LookUp` method and extend the **Anime** and **Manga** nodes with an additional `TotalRating` property. </br>
5. The **Fusion Gateway** is not designed to handle auth by itself. It is the responsibility of the sub-graphs to do that.
6. The **Fusion Gateway** will forward the Auth request headers to the sub-graphs for them to handle auth.


### Setup

- To set up required templates for future use, run:
  ```bash
  dotnet new install HotChocolate.Template

## Additional resources
- [Official documentation](https://chillicream.com/docs/fusion/v14)
- [Fusion and Aspire](https://youtu.be/AHitpPCeM00?si=D6H_d7Ocdel4-zmX)