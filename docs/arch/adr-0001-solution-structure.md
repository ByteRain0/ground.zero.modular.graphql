# 0001. Title: Solution structure

**Date:** 2024-12-23

# Problem

Define the structure both logical and physical of the project.

# Decision

The solution is going to use a modular-monolith structure.
Every module is going to be composed of:
* Contracts class library. 
* Service class library. 
* GraphQL class library.
* (Optional) Bridge class library.

General solution class libraries:
* Common. 
* Core. 
* Japanese.Api.

Additional elements to take into account:
* Directory.Build.props - centralised project configurations.
* Directory.Packages.props - centralised NuGet management.
* docker-compose.yaml - local development infrastructure provisioning.

## Contracts

Each module will expose a Public API for integration.
Modules inside the system will work with the contracts library whenever they need to consume or send information to the module.
Here we will keep Interfaces, Models, Events and Validators for each module.

This is the desired structure of the contracts class library:
* Exceptions -- contains a list of exceptions that the module can throw.
* Models -- contains a list of domain models and integration events specific to the module.
  * Events
    * Notifications -- contains events the clients can subscribe to.
      * Event1.cs
      * ..
    * Integrations -- contains events other systems components can subscribe to.
      * Event2.cs
      * ..
  * Model1.cs
  * ..
* Services
  * Node1 -- contains all commands / queries / metrics specific to a Node (domain entity) in the module. 
    * Commands
      * Command1.cs + Command1Validator.cs
      * ..
    * Queries
      * Query1.cs + Query1Validator.cs + Query1Filters.cs
    * Metrics
      * NodeMetrics.cs
    * Telemetry -- contains telemetry tags specific for to a Node.
      * NodeTelemetryTags.cs
  * Node2
    * ..

## Service
Every module will have an implementation class library. Here is where most of the business operations will be handled.
Each module can have a unique structure in order to cater to the specific needs of the business.
The following description is the standard structure of a service class library.

* Application
  * Node1
    * CommandHandlers
      * Command1
        * Command1Handler.cs
        * Event1NotificationHandler.cs -- 'NotificationHandler' denotes that this class pushes real-time notifications as SSE to clients.
        * .. additional internal models if required
    * QueryHandlers
      * Query1
        * QueryHandler1.cs
        * .. additional internal models if required
    * EventHandlers
      * Event1
        * EventHandler1.cs -- traditional cross module async event handler.
  * Node2
  * ..
* Infrastructure
  * Data
    * Configurations  
      * Model1Configuration.cs -- EFCore configuration of the model.
    * DataLoaders
      * Node1DataLoader.cs -- Data loader that integrates with EFCore for easier data extraction.
    * Migrations
      * ..
    * Seed
      * Model1Seed.cs
      * Model2Seed.cs
      * ..
    * ModuleDbContext.cs
  * ModuleInfo.cs -- Information for the source generation engine of HotChocolate
  * ServiceCollectionExtensions.cs -- DI extension for Program.cs

## GraphQL
* Node1
  * Mutations
    * Mutation1.cs + CommandInputType.cs + (optionaly) CommandResponseType.cs
  * Queries
    * Query1.cs
  * Nodes
    * ModelNode1.cs -- Representation of a specific Node in GraphQl.
    * .. - additioanl GraphQl specific types like constraints.cs enums etc.
* Node2
  * ..
* Infrastructure
  * 

## Bridge
A class library meant for merging the query functionalities of 2 or more modules at GraphQL level.

## Common
A class library used for common module domain elements.

## Core
A class library used for common infrastructure elements. Like:
* Auth
* Behaviors
* Configurations
* Exceptions
* Pagination
* Messaging
* QueryFilters
* etc.

## Japanese.Api

Host app for the functionality. Is responsible for DI, Configurations and running the app.

# Consequences

Positive:
* Well-defined structure and boundaries for the project.
* Easier maintenance due to decoupling of the modules.
* Structure can be enforced with Architecture tests later on.

Negative:
* Increased difficulty in developing the system.
* A steeper learning curve.
* Additional boilerplate code.