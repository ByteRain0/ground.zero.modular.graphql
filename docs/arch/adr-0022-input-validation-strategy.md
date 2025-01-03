# 0022. Input Validation Strategy

**Date:** 2025-01-03

## Problem

The API needs a mechanism to validate data received from clients.

---

## Decision

We will use the **FairyBread** and **FluentValidation** NuGet packages to implement input validation for the API. </br>  
This approach provides integrated behavior, where **ValidationExceptions** will be converted into explicit GraphQL errors. </br>  
Additionally, the **ValidationException** class will be reused for business-related validation checks, such as duplicate names.

---

## Consequences

1. Validation will occur only at the system's entry points.
2. Public validators must be created for all commands/queries to ensure they are registered by **FairyBread**.
3. For business rule violations (e.g., duplicate names), throw a **ValidationException**. The **BusinessValidationErrorFilter** will catch these exceptions and transform them into user-friendly errors for the client. 
4. Validation beyond the API's entry points (e.g., across method calls) will be skipped under the assumption that our internal code is reliable.  
