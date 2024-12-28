# 0008. Running jobs in background

**Date:** 2024-12-26

## Problem

The system needs a capability to run specific operations in the background.

## Decision

We will use Hangfire to run certain operations asynchronously in the background. </br>
The api itself will also 'do the work' i.e. be configured as a Hangfire server to execute the operation. </br>
In the future we might opt to create a console application that will take the place of the Hangfire server.

## Consequences

Positive:
1. The system will be able to run operations in the background.
2. Hangfire api is relatively simple to use and configure.
3. Hangfire has integration with postgresql databases to store information about the jobs and runs.
4. If required we can enhance the MediatR api with an enqueue method that will run requests in the background. -This will be tackled in a separate ADR.

Negatives:
1. Added another dependency into the system.
2. Additional learning curve for new developers on the team.
