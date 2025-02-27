# 10. Auth strategy

Date: 2025-02-22

## Status

Proposed

## Context

We need a mechanism to enforce **role-based authorization** at the application layer of the **Japanese.Api**. </br>


## Decision

A custom **MediatR pipeline behavior** will be implemented to handle authorization logic. </br>

## Consequences / Tech aspects

1. **Role Annotation:** </br>
    - **MediatR IRequests** will be annotated with the `AuthorizeRolesAttribute`, specifying the required roles for each request. </br>
2. **Pipeline Behavior:** </br>
    - The `AuthorizationBehavior` will be executed for all **MediatR requests**. </br>
    - Note: This approach may lead to **performance degradation** if the pipeline is executed multiple times during a single client-server request. </br>
3. **Exception Handling:** </br>
    - If the user does not have the required roles, the `AuthorizationBehavior` will raise a `ForbiddenException` with a detailed message. </br>
    - This exception will be caught by the `GraphQLAuthExceptionFilter` and transformed into a **client-consumable error**. </br>
4. **Role Management:** </br>
    - **Keycloak** defines roles per application, allowing a user to have multiple role accesses to different applications within a single authentication token. </br>
5. **Scope:** </br>
    - Only **role-based authorization** is currently in scope for the **Japanese.Api**. </br>

### Notes

In the future, we may explore: </br>
- The **authorization setup introduced in HotChocolate 13**. </br>
- Using **OPA (Open Policy Agent)** for advanced policy evaluations. </br>