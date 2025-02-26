# 0003. Anime & Manga Module Structures

**Date:** 2024-12-25

## Problem

Explain the rationale behind implementing the Anime and Manga modules in different ways and their respective design decisions.

---

## Decision

- The **Anime module** will follow a more traditional, albeit simplified, Onion Architecture template. In this setup, GraphQL will primarily act at the API layer.
- The **Manga module** will adopt a **GraphQL over Database** (EF Core) approach, directly exposing `IQueryables` to clients, allowing them to build their queries dynamically.
- A **Bridge module** will be introduced to unify the public APIs of both modules at the GraphQL level.

---

## Consequences

### Anime Module



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
