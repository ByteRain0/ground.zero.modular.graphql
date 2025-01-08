# 0034. Distributed GraphQL

**Date:** 2025-01-08

## Problem

Users searching for a new anime or manga to watch/read want to see ratings for the anime/manga. </br>

---

## Decision

A new **Rating.Api** will be created to encapsulate the logic related to ratings. </br>  
To simplify integration for front-end and other consumers, a **Fusion Gateway** will be placed in front of the **Japanese** and **Rating.Api**. </br>

---

## Consequences

1. The **Rating.Api** will manage all rating-related logic for anime and manga. </br>
2. The initial implementation will utilize an in-memory database seeded with ratings via migrations. </br>
3. A **Fusion Gateway** built using Fusion and Aspire will enable a distributed GraphQL schema. </br>
4. **Rating.Api** will introduce an internal `LookUp` method and extend the **Anime** and **Manga** nodes with an additional `TotalRating` property. </br>

---

### Additional Notes

- To set up required templates for future use, run:
  ```bash
  dotnet new install HotChocolate.Template
