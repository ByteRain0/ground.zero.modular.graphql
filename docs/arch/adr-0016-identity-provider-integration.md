# 0016. Identity Provider Integration

**Date:** 2025-01-02

## Problem

The application requires a robust mechanism for authentication and authorization.

---

## Decision

We have chosen **Keycloak** as the identity provider. </br>  
Keycloak is free of charge and integrates seamlessly with **Aspire**.

---

## Consequences

### Configurations

1. **Keycloak** will serve as the identity provider for the API. </br>
2. For local development, a preconfigured instance of Keycloak will be started using **Aspire**. </br>
3. Realm configurations will be stored in the `./configurations/keycloak` directory.

---

### Authentication

Authentication will initially be handled directly by Keycloak. </br>  
In the future, we may decide to shift authentication methods to the API itself.

---

### Authorization

1. Role-based authorization will be used for now, as entitlements/permissions are unnecessary for the project's current scope. </br>
2. A custom attribute, **`AuthorizeRoles`**, will be implemented to accept a list of roles for authorization checks. </br>
3. The **`AuthorizeRoles`** attribute will be applied in the **Contracts** class libraries on **MediatR requests**. </br>
4. A **MediatR pipeline behavior** will be created to verify user roles against the roles specified in the attribute.

---

### Entity-Based Permissions

Fine-grained control checks for entity-based permissions will be addressed in a separate ADR.  
