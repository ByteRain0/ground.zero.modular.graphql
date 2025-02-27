# 8. Observability strategy

Date: 2025-02-22

## Status

Accepted

## Context

The solution requires a robust observability setup to ensure effective monitoring, debugging, and performance tracking.

## Decision

We will adopt the OpenTelemetry observability stack to gather signals (logs, spans, metrics) from the system.  
Signals will be pushed from the API to an OTel collector, which will forward them to the observability backends.

## Consequences

### Architecture Setup

- **Current Setup**:  
  The API will push signals to a pre-configured OTel collector.  
  For local development, we will utilize Aspire Dashboard as an aggregation solution.

- **Live Environment**:  
  The live setup will be more comprehensive and will include:
    1. OTel Collector
    2. Loki - Log aggregation solution
    3. Prometheus - Metrics and monitoring solution
    4. Tempo - Tracing backend solution
    5. Grafana - Observability platform

- **Local Development**:  
  The OTel collector instance will run as part of the Aspire app host.

- **Configuration Management**:  
  Live environment configurations for the collector and services will be stored in the `./configurations` folder.

### Configurations

- **API Configuration**:  
  OTel configurations will be maintained in the Core class library.  
  The `RunTimeDiagnosticConfig` will be used to create custom spans and metrics across the solution.  
  `MediatR` behaviors will be utilized to generate additional logs and spans related to request execution.  
  Additional instrumentation libraries will be included to increase the volume and granularity of signals collected:
    1. HotChocolate.Diagnostics
    2. Npgsql.OpenTelemetry
    3. OpenTelemetry.Instrumentation.EntityFrameworkCore
    4. etc.

### Observability Strategy

1. Treat logs as human-readable audit events.
2. Use spans to provide additional context that complements the logs, offering a clearer understanding of events.
3. Collect metrics to capture relevant usage data about the application.
4. Leverage signals for setting up alerts in case of issues.
5. **Trace Processing Strategy**:
    - Filter out PII data.
    - Sample only 10% of health checks.
    - Sample only 10% of Hangfire probes.
    - Sample all traces for requests with errors.
    - Sample all traces for requests exceeding the expected NFR (±500ms).
    - ...

6. Use unit tests to validate that the expected signal formats are consistently maintained.

## Additional resources:
1. [OpenTelemetry documentation](https://opentelemetry.io/)
2. [OpenTelemetry Collector Configuration](https://opentelemetry.io/docs/collector/configuration)
3. [OpenTelemetry Tail Sampling Processor](https://github.com/open-telemetry/opentelemetry-collector-contrib/blob/main/processor/tailsamplingprocessor/README.md)
4. [Practical open telemetry workshop](https://github.com/martinjt/practical-otel-workshop/tree/main)
5. [OTel collector configuration validator](https://www.otelbin.io/)