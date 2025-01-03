# 0000. Record Architecture Decisions

**Date:** 2024-12-22

## Problem

To maintain a clear and traceable history of architectural decisions, we need a standardized way to document them and store them as close to the codebase as possible.

---

## Decision

We will adopt the concept of Architecture Decision Records (ADRs), as [described by Michael Nygard](http://thinkrelevance.com/blog/2011/11/15/documenting-architecture-decisions).  
These records will serve as a structured and lightweight mechanism for documenting decisions.

---

## Consequences

- **New Decisions**:  
  Whenever a new architectural decision is made, a corresponding ADR must be created.
- **Immutability**:  
  Existing ADRs will remain immutable. If a previous decision needs revisiting or updating, a new ADR will be created to reflect the change while preserving historical context.

### Additional Resources

- Michael Nygard's article: [Documenting Architecture Decisions](http://thinkrelevance.com/blog/2011/11/15/documenting-architecture-decisions)
- Lightweight ADR toolset: [adr-tools by Nat Pryce](https://github.com/npryce/adr-tools)
