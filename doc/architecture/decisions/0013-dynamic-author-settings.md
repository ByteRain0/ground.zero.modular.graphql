# 13. Dynamic author settings

Date: 2025-02-22

## Status

Proposed

## Context

The system occasionally needs to handle dynamic data structures inferred from JSON files or databases. </br>  
There must be a way to dynamically infer schema types and hot-reload the schema whenever changes occur.

## Decision

We will use **HotChocolate's ITypeModule** to integrate dynamic types into the GraphQL schema. </br>

## Consequences


### Proof of Concept (PoC)

1. A PoC field named **Settings** will be added to the **Author** node. </br>
2. The structure of the **Settings** object will be sourced from configuration files to simplify management. </br>

---

### Implementation

3. An **AuthorSettingsModule** will be developed to:
    - Read the structure of settings from the configuration. </br>
    - Translate the settings into an **extension** on the **AuthorNode**. </br>

---

### Monitoring and Hot Reload

4. The **AuthorSettingsModule** will monitor the configuration for changes. </br>
5. When a change occurs:
    - Existing requests will be completed. </br>
    - The schema will be updated dynamically to reflect the changes.  
