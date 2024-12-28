# 0000. Record Architecture Decisions

**Date:** 2024-12-22

## Problem

We need to record the architectural decisions made on this project and store them as close to the code as possible.

## Decision

We will use the concept of Architecture Decision Records, as [described by Michael Nygard](http://thinkrelevance.com/blog/2011/11/15/documenting-architecture-decisions).

## Consequences

- Every time a new architecture decision is made, it is required to add a new Architecture Decision Record.
- Existing Architecture Decision Records are immutable. Whenever there is a need to update an old record, a new one is created.

See Michael Nygard's article, linked above. For a lightweight ADR toolset, see Nat Pryce's [adr-tools](https://github.com/npryce/adr-tools).
