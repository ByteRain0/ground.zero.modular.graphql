# 0031. Notifying clients about occured events

**Date:** 2024-12-24

## Problem

The clients want to subscribe to real time notifications about newly added anime.

## Options

Following options have been considered:
1. SignalR
2. GraphQL subscriptions (ServerSideEvents (SSE) and WS websockets)

## Decision

Use GraphQL subscriptions (SSE) with the Hotchocoloate.Subscriptions.* NuGet packages and InMemory subscription provider.
Unlike WebSockets, SSE is a half-duplex communication channel, which means the server can send messages to the client, but not the other way around. This makes it a good fit for one-way real-time data like updates or notifications.
SignalR is a valid alternative which is based around the .NET ecosystem but we are looking a more universal solution.


## Consequences

Positive:
1. Will allow clients to subscribe to specific events.
2. In the code we can use ITopicEventSender to send events from anywhere in the system.
3. HotChocolate has an abstraction that will allow us to change the implementation details of the subscription provider in the future. 
4. Current subscription provider options that we have are : InMemory, Redis & RabbitMq.

Negative:
1. We are going to couple to HotChocolate abstractions.
2. No duplex communication.

