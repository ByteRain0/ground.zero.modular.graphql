# 0003. Anime & Manga Module Structures

**Date:** 2024-12-25

## Problem

Explain the rationale behind implementing the Anime and Manga modules in different ways and their respective design decisions.

## Decision

- The **Anime module** will follow a more traditional, albeit simplified, Onion Architecture template. In this setup, GraphQL will primarily act at the API layer.
- The **Manga module** will adopt a **GraphQL over Database** (EF Core) approach, directly exposing `IQueryables` to clients, allowing them to build their queries dynamically.
- A **Bridge module** will be introduced to unify the public APIs of both modules at the GraphQL level.

---

## Consequences

### Anime Module

1. **Complexity**:  
   The Anime module will be more complex due to the layered Onion Architecture design.

2. **Boilerplate Code**:  
   Requires more boilerplate code to ensure a clean separation of concerns.

3. **Boilerplate Mitigation**:  
   To reduce redundancy:
   - **Contracts Library as Domain Layer**: The Contracts library will act as the Domain layer, housing domain models.
   - **Domain Models Reuse**: Domain models will be reused in the EF Core `DbContext`.
   - **EF Core Configurations**: Mappings will be handled via configurations to efficiently map domain models to the database.
   - **GraphQL Nodes**: Nodes will also be based on domain models, with configurations used to fine-tune the exposed data and operations.

4. **Schema Control**:  
   This approach provides finer control over the GraphQL schema, ensuring it aligns with business requirements.

5. **Contracts Library**:  
   The Contracts library will expose:
   - Events for **client subscriptions** (notifications).
   - Events for **module integrations** (integrations).

---

### Manga Module

1. **Simplified Backend Development**:  
   The Manga module will have a simpler structure, making it easier for backend developers to work with.

2. **Increased Frontend Responsibility**:  
   Frontend developers will need to invest more effort into building queries dynamically from the exposed GraphQL nodes.

3. **Greater API Freedom**:  
   Clients will have more flexibility in how they query and interact with the API, enabling a broader range of use cases.

---

## Summary

The Anime module prioritizes control and structure, making it ideal for scenarios requiring a tightly managed schema and event-driven integrations. The Manga module prioritizes simplicity and flexibility, better suited for client-driven query-building use cases. The Bridge module ensures that both approaches coexist seamlessly within the same GraphQL endpoint.  
