# 0014. Observability setup

**Date:** 2024-12-26

## Problem

The solution is in need of a robust observability setup.

## Decision

We will use the OpenTelemetry observability stack to gather signals (logs, spans, metrics) from the system. </br>
Signals will be pushed from the api to a OTel collector towards the observability backends. </br>

## Consequences

### Architecture setup

In the current setup we will have the api push signals to a pre-configured OTel collector.
For local development we will use Aspire Dashboard as an aggregation solution.
For live we will create a more extensive setup consisting of:
1. OTel collector
2. Loki - log aggregation solution.
3. Prometheus - metrics & monitoring solution.
4. Tempo - tracing backend solution.
5. Grafana - observability platform.

For both setups a specific docker-compose file will be created. </br>
Configurations will be held in a ./configurations folder.

### Configurations

For the api the OTel configurations will be held inside the Core class library. </br>
The RunTimeDiagnosticConfig will be used to create custom spans and metrics throughout the solution. </br>
MediatR behaviors will be leveraged to generate additional logs/spans in regards to current request execution. </br>
Additional instrumentation libraries will be installed to increase the amount of signals we can gather. </br>
Libraries:
1. HotChocolate.Diagnostics - GraphQl instrumentation.
2. Npgsql.OpenTelemetry - Postgresql instrumentation.
3. OpenTelemetry.Instrumentation.EntityFrameworkCore 
4. Masstransit - instrumentation

#### Additional configuration links
1. https://opentelemetry.io/docs/collector/configuration
2. https://github.com/open-telemetry/opentelemetry-collector-contrib/blob/main/processor/tailsamplingprocessor/README.md

### Observability strategy

1. Treat logs as human-readable audit events.
2. Treat spans as additional information that enhances the logs to better understand what went on during a specific event.
3. Use metrics to capture relevant usage data about the application.
4. Use signals to set up alerting in case something goes wrong.
5. Trace processing strategy:
   * Add filter to remove PII data.
   * Add rule to sample only 10% of the health checks.
   * Add rule to sample only 10% of the hangfire probes.
   * Add rule to sample all traces in case there was an error.
   * Add rule to sample all traces in case the request took longer than expected NFR (±500ms). 
   * 
6. Use Unit tests to ensure that the expected signal formats are maintained.
