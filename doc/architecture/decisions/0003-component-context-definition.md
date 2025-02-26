# 3. Component context definition

Date: 2025-02-22

## Status

Accepted

## Context

Create a C4 component level overview of the Japanese.Api.
Explain the rationale behind implementing the Anime and Manga modules in different ways and their respective design decisions.


## Solution

Create a C4 component level diagram covering the Anime & Manga modules of the Japanese.Api.

## Additional details

### High level decisions
- The **Anime module** follows a more traditional, albeit simplified, Onion Architecture template. In this setup, GraphQL will primarily act at the API layer.
- The **Manga module** adopts a **GraphQL over Database** (EF Core) approach, directly exposing `IQueryables` to clients, allowing them to build their queries dynamically.
- A **Bridge module** will be introduced to unify the public APIs of both modules at the GraphQL level.

---

### Anime module

#### Anime contracts library structure:
- **Exceptions**: Lists exceptions specific to the module.
- **Models**: Contains domain models and integration events.
   - **Events**
      - Events for client subscriptions.
      - Events for inter-module or system-wide subscriptions.
- **Services**
   - Organized by **Node** (domain entities), containing commands, queries, metrics, and telemetry tags.
      - **Commands**: Command definitions and validators.
      - **Queries**: Query definitions, validators, and filters.
      - **Metrics**: Metrics for the node.
      - **Telemetry**: Tags specific to the node for observability.

#### Anime GraphQL library structure:
- **Nodes**: Represents domain entities as GraphQL types.
- **Mutations**: Command inputs and (optional) response types.
- **Subscriptions**: Subscription types.
- **Queries**: Query types.
- Additional GraphQL-specific definitions like constraints and enums.

#### Anime service library structure:
- **Application**
   - **CommandHandlers**: Handles business operations for commands.
   - **QueryHandlers**: Handles business operations for queries.
   - **EventHandlers**: Handles async cross-module events.
- **Infrastructure**
   - **Data**
      - **Configurations**: EF Core model configurations.
      - **DataLoaders**: Integrate EF Core for data fetching.
      - **Migrations**: Database migrations.
      - **Seed**: Data seeding classes.
      - **ModuleDbContext**: Database context for the module.
   - **ModuleInfo.cs**: Metadata for HotChocolate source generation.
   - **ServiceCollectionExtensions.cs**: Dependency Injection setup.

   
#### Boilerplate mitigation
- **Contracts Library as Domain Layer**:
- **Domain Models Reuse**: Domain models will be reused in the EF Core `DbContext`.
- **EF Core Configurations**: Mappings will be handled via configurations to efficiently map domain models to the database.
- **GraphQL Nodes**: Nodes will also be based on domain models, with configurations used to fine-tune the exposed data and operations.

---

### Manga module

#### Manga contracts library structure:
- **Models**: Contains domain models and integration events.
- **Services**
   - Organized by **Node** (domain entities), containing queries, metrics, and telemetry tags.
      - **Queries**: Query definitions, validators, and filters.

#### Anime GraphQL library structure:
- **Nodes**: Represents domain entities as GraphQL types.
- **Queries**: Query types.
- Additional GraphQL-specific definitions like constraints and enums.

#### Manga service library structure:
- **Application**
    - **QueryHandlers**: Handles business operations for queries and exposes EF Core integration to GraphQL queries (engine).
- **Infrastructure**
    - **Data**
        - **Configurations**: EF Core model configurations.
        - **Migrations**: Database migrations.
        - **Seed**: Data seeding classes.
        - **Dynamic Types**: Author settings have a dynamic structure that can be exposed via GraphQL. 
        - **ModuleDbContext**: Database context for the module.
    - **ModuleInfo.cs**: Metadata for HotChocolate source generation.
    - **ServiceCollectionExtensions.cs**: Dependency Injection setup.