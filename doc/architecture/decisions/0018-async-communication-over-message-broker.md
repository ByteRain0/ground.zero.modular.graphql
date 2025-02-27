# 18. Async communication over message broker

Date: 2025-02-22

## Status

Accepted

## Context

The **Rating.Api** (and potentially other components in the future) needs to be informed when a new anime is added. </br>
We require a solution that supports **loose coupling** between components while ensuring high levels of **maintainability**. </br>

## Decision

We will implement **RabbitMQ** as the message broker and use **MassTransit** for integration. </br>
The `App.Host` managed by Aspire will handle spinning up the RabbitMQ instance. </br>

### Technical Implementation

1. **Message Publishing:** </br>
    - Messages will be sent to RabbitMQ through a dedicated interface called `IMessageSender`. </br>
    - The `IMessageSender` interface will publish messages via MassTransit, making it easy to stub for testing purposes. </br>

2. **Initial Use Case:** </br>
    - **Japanese.Api** will publish an `anime:created` event whenever a new anime is added. </br>
    - **Rating.Api** will subscribe to this event and act accordingly. </br>

3. **Contract Handling:** </br>
    - To simulate external dependency, the `anime:created` contract will be recreated inside the **Rating.Api** namespace. </br>
    - This approach mimics receiving the contract via NuGet and isolates cross-service dependencies. </br>


## Consequences


### Benefits

1. **Loose Coupling:** </br>
    - Services can communicate without tight integration, promoting maintainability and scalability. </br>

2. **Testing Flexibility:** </br>
    - The use of `IMessageSender` allows for easy stubbing during tests, simplifying validation of services. </br>

3. **Extensibility:** </br>
    - The setup can be expanded to other components in the system as needed. </br>

4. **Decoupled Contracts:** </br>
    - Mimicking external contracts inside the consumer service (e.g., Rating.Api) reduces dependency on shared internal libraries. </br>

### Drawbacks

1. **Additional Complexity:** </br>
    - Setting up RabbitMQ and integrating with MassTransit introduces more infrastructure and code dependencies. </br>

2. **Message Broker Overhead:** </br>
    - Using RabbitMQ requires monitoring and maintenance to ensure reliability. </br>



## Additional resources

1. [RabbitMq with Masstransit](https://youtu.be/NIi0DrUM1J0?si=mnP9oIFizLxT0-9R) </br>
2. [Async communication patterns](https://youtu.be/XdpNXGqny9c?si=DbBOxb-iXikvhlpY) </br> 