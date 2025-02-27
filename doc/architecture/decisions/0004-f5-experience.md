# 4. F5 Experience

Date: 2025-02-22

## Status

Accepted

## Context

New developers should be able to easily set up and run the solution on a clean machine.

## Decision

Adopt Aspire to streamline the local development process.

## Consequences

- Aspire will handle infrastructure provisioning for local development.
- Developers will use Aspire and Aspire-Integrations to set up system containers.
- Docker Compose will be deprecated in favor of Aspire and C# configuration.
- Developers will use an App.Host to run the system locally. 2 Launch profiles will be created:
  - single-project : which will run only the Japanese.Api and it's dependencies
  - http/https : which will run Japanese.Api, Rating.Api and Fusion Gateway.
- Telemetry configuration will be done via an OTel collector to resemble the deployment model.
- Deployment artifacts/manifests will be generated using the Aspirate project until Aspire provides built-in support for Kubernetes.