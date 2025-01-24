# 0001. Solution Structure

**Date:** 2024-12-23 </br>
**Last-Updated:** 2025-01-08

## Problem

Define both the logical and physical structure of the project to ensure consistency, scalability, and maintainability.

---

## Decision

The solution will follow a **modular-monolith architecture**. Each module will consist of:
- **Contracts** class library
- **Service** class library
- **GraphQL** class library
- (Optional) **Bridge** class library

### General Solution Class Libraries:
- **Common**: Shared domain elements across modules.
- **Core**: Shared infrastructure elements such as authentication, behaviors, and configurations.
- **Japanese.Api**: The API application responsible for dependency injection (DI), configurations, and running the app.
- **App.Host**: Aspire host application responsible for infrastructure setup.
- **ServiceDefaults**: Aspire base service defaults.

### Additional Elements:
- **Directory.Build.props**: Centralized project configurations.
- **Directory.Packages.props**: Centralized NuGet dependency management.

---

## Module Structure

### **Contracts**
Defines the module's public API for integration, ensuring clear boundaries and communication protocols between modules.

#### Desired Contracts Library Structure:
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

---

### **Service**
Contains business logic and implementation details for each module.

#### Standard Service Structure:
- **Application**
  - **CommandHandlers**: Handles business operations for commands.
  - **QueryHandlers**: Handles business operations for queries.
  - **EventHandlers**: Handles async cross-module events.
- **Infrastructure**
  - **Data**
    - **Configurations**: EF Core model configurations.
    - **DataLoaders**: Integrates EF Core for streamlined data access.
    - **Migrations**: Database migrations.
    - **Seed**: Data seeding classes.
    - **ModuleDbContext**: Database context for the module.
  - **ModuleInfo.cs**: Metadata for HotChocolate source generation.
  - **ServiceCollectionExtensions.cs**: Dependency Injection setup.

---

### **GraphQL**
Defines GraphQL types, queries, and mutations for each module.

#### Structure:
- **Nodes**: Represents domain entities as GraphQL types.
- **Mutations**: Command inputs and (optional) response types.
- **Queries**: Query types.
- Additional GraphQL-specific definitions like constraints and enums.

---

### **Bridge**
Facilitates the merging of GraphQL query functionalities across multiple modules.

---

## Shared Libraries

### **Common**
Houses shared domain elements used across modules.

### **Core**
Houses shared infrastructure elements, such as:
- Authentication
- Behaviors
- Configurations
- Exceptions
- Pagination
- Messaging
- Query Filters
- Aspire Service Defaults

---

## Japanese.Api
The api application responsible for:
- Dependency Injection (DI).
- Centralized configurations.
- Running the application.

---

## Japanese.Api.MigrationService
Worker responsible for running migrations for:
- AnimeDbContext
- MangaDbContext

---

## Rating.Api
The api application responsible for:
- Storing ratings for anime and manga.

---

## Gateway

Fusion based api gateway, responsible for:
- Creating a single entry point for the graph.
- Merging the sub-schemas of Japanese.Api and Rating.Api into one single schema.

---

## App.Host
The Host application responsible for:
- Infrastructure pre-provisioning
- Environment configurations
- Running the system locally

---

## Consequences

### Positive:
- Provides a clear and well-defined structure with module boundaries.
- Simplifies maintenance through decoupling.
- Supports enforcement of the architecture via automated tests.

### Negative:
- Increased complexity in development.
- Steeper learning curve for new team members.
- Additional boilerplate code required.
