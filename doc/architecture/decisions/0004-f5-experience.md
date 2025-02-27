# 4. F5 Experience

Date: 2025-02-22

## Status

Accepted

## Context

New developers should be able to easily set up and run the solution on a clean machine.

## Decision

We will use `.NET Aspire` to enhance the local development environment, as it provides integrated tools and capabilities tailored for .NET applications.

## Alternatives

**Docker Compose**: While Docker Compose provides a robust way to orchestrate containers, it requires additional configuration and maintenance overhead, which may complicate the development process for this specific use case.


## Consequences

- Local development will be more straightforward and aligned with .NET-specific workflows.
- Aspire will handle infrastructure provisioning for local development.
- Faster and less complex setup process.
- Developers will be provided with 2 launch profiles:
  - single-project : which will run only the Japanese.Api and it's dependencies
  - http/https : which will run Japanese.Api, Rating.Api and Fusion Gateway.
- Dependency on .NET Aspire tools means the team must stay up-to-date with its latest updates and best practices.
- Deployment artifacts/manifests will be generated using the `Aspirate` project until native Kubernetes support is provided.


For further details, see the documentation for .NET Aspire and compare it with other solutions like Docker Compose if needed.