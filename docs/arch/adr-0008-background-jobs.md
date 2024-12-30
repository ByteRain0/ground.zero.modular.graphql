# 0008. Running Jobs in the Background

**Date:** 2024-12-26

## Problem

The system requires the ability to execute specific operations in the background, ensuring that these tasks do not block the main application flow.

## Decision

We will utilize **Hangfire** to manage and execute asynchronous operations in the background. </br>  
Initially, the API itself will serve as the **Hangfire server**, responsible for executing background operations. </br>  
In the future, we may replace the API server with a dedicated **console application** to act as the Hangfire server.

---

## Consequences

### Positive:
1. Enables the system to efficiently run background operations.
2. **Hangfire API** is straightforward to configure and use.
3. Integrates seamlessly with PostgreSQL, providing robust job tracking and persistence.
4. Offers flexibility for future enhancements, such as adding an `enqueue` method to the **MediatR API** for handling background requests (to be addressed in a separate ADR).

### Negative:
1. Introduces an additional dependency into the system.
2. New team members may face a learning curve in understanding and using Hangfire.  
