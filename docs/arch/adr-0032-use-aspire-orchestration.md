# 0032. Easier Local Development Experience

**Date:** 2024-12-30

## Problem

We need to simplify the local development experience to improve developer productivity and reduce setup time.

---

## Decision

We will use `.NET Aspire` to enhance the local development environment, as it provides integrated tools and capabilities tailored for .NET applications.

---

## Alternatives

- **Docker Compose**: While Docker Compose provides a robust way to orchestrate containers, it requires additional configuration and maintenance overhead, which may complicate the development process for this specific use case.

---

## Consequences

- Local development will be more straightforward and aligned with .NET-specific workflows.
- Faster and less complex setup process.
- Dependency on `.NET Aspire` tools means the team must stay up-to-date with its latest updates and best practices.

For further details, see the documentation for `.NET Aspire` and compare it with other solutions like Docker Compose if needed.
