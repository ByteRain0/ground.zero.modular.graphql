# 17. Entity framework and database integration

Date: 2025-02-22

## Status

Accepted

## Context

We need a reliable data storage solution for managing anime and manga data. </br>

## Decision

We will use **PostgreSQL** as the primary data store for the application. </br>

## Consequences

1. For local development, a single instance of the database will be spun up via **Aspire**, named **default-db**. </br>
   **OTel** (OpenTelemetry) will be configured using **ServiceDefaults**. </br>
2. Both modules (**Anime** and **Manga**) will share the same physical database but will use separate EF Core contexts. </br>
3. The **AppHost** will manage the connection string and pass it to the API. </br>
4. **Migrations**: </br>
    - At design time (e.g., during migration creation), a connection to the database is required. Since the database runs via Aspire, a workaround is needed. </br>
    - Custom **ContextDesignTimeFactory** classes will be created for each EF Core context. </br>
    - A project reference will be added from the context's class library to the **AppHost**, like this:
      ```xml
      <ProjectReference Include="..\..\Modules\Anime\Anime.Service\Anime.Service.csproj" IsAspireProjectResource="false" />
      ```  
5. Data seeding will be handled via **EF Core migrations**. </br>
6. Entity configurations will be implemented using the **Fluent API**. </br>
7. A separate worker service **Japanese.Api.MigrationService** will be created to apply migrations and run it via Aspire. </br>
8. The **Application Layer** will reference the **DbContext** directly to eliminate unnecessary abstraction layers. </br>
9. The use of **DataLoaders** in conjunction with EF Core contexts will be described in a separate ADR. </br>

## Additional Resources

1. [Adding EF Core Migrations to .NET Aspire Solutions](https://khalidabuhakmeh.com/add-ef-core-migrations-to-dotnet-aspire-solutions) </br>
2. Example command to add a migration from the root:
   ```bash
   dotnet ef migrations add UserSettings --project src/Modules/Manga/Manga.Service/Manga.Service.csproj --startup-project src/Host/App.Host/App.Host.csproj --context MangaDbContext
