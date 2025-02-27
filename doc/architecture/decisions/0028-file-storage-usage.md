# 28. File storage usage

Date: 2025-02-27

## Status

Proposed

## Context

The issue motivating this decision, and any context that influences or constrains the decision.

## Decision

The change that we're proposing or have agreed to implement.

## Consequences

1. Options : Local storage / MinIo
2. Graphql multi part request to upload an image
3. Mutations module last tutorial.
4. Do not serve image as byte[] in the response. Send an URI let the browser download images in paralel for faster response.
5. Create an MinimalAPI endpoint to serve the images.