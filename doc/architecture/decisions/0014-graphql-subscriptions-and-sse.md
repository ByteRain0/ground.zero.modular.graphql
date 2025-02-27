# 14. GraphQL Subscriptions and SSE

Date: 2025-02-22

## Status

Accepted

## Context

Clients require the ability to subscribe to real-time notifications for newly added anime.

## Options

The following options were considered:
1. **SignalR**
2. **GraphQL Subscriptions** using Server-Sent Events (SSE) or WebSockets

## Decision

We will implement **GraphQL subscriptions (SSE)** using the **HotChocolate.Subscriptions.* NuGet packages** and the **InMemory subscription provider**. </br>  
SSE offers a half-duplex communication channel where the server can send messages to the client but not vice versa, making it ideal for one-way real-time data such as updates or notifications. </br>  
While SignalR is a viable alternative and well-integrated into the .NET ecosystem, we opted for a more universal solution provided by HotChocolate.

## Consequences

### Positive:
1. Enables clients to subscribe to specific events for real-time updates.
2. Supports the use of **`ITopicEventSender`**, allowing events to be triggered from anywhere in the system.
3. HotChocolate provides an abstraction layer, making it easier to change the subscription provider implementation in the future.
4. Available subscription providers include: **InMemory**, **Redis**, and **RabbitMQ**.

### Negative:
1. Introduces coupling to HotChocolate abstractions.
2. Does not support duplex communication (client-to-server messages).  