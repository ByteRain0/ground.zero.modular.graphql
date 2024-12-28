# 0003. Anime & Manga module structures

**Date:** 2024-12-25

## Problem

Explain why Anime & Manga modules are implemented in a different way.

## Decision

Anime module is going to resemble a more 'traditional', albeit simplified, Onion-Architecture template with GraphQL mostly acting at the API layer. </br>
Manga module is going to resemble a GraphQL over Database (EfCore) structure, with direct exposure of IQueryables for clients to allow them to build their queries. </br>
A bridge module will be in place that will unite the public API's of both modules at graphql level.

## Consequences

Anime module:
1. Is going to be more complex.
2. Require more boilerplate code.
3. To mitigate the need for boilerplate code we will do the following:
   * Treat Contracts class library as Domain layer.
   * Domain models (i.e. entities) will be r-used in EfCore DbContext.
   * EfCore will use Configurations to map domain models to the database more efficiently.
   * GraphQl nodes will be also based on the domain models and use configurations to fine tune what data and what operations we expose.
4. This will allow us more fine-grained control over how our schema will look like.
5. Contracts library will expose a set of events for client subscriptions (notifications) and other modules consumption (integrations).


Manga module:
1. Is going to be simpler for BE developers.
2. Is going to require more effort from FE developers to build their queries from the exposed Nodes.
3. Allows more usage 'freedom' for clients of the API.