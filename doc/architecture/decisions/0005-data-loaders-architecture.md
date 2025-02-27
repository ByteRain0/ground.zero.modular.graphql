# 5. Data loaders architecture

Date: 2025-02-22

## Status

Accepted

## Context

Every data fetching technology is prone to suffer from n+1 problem. </br>
The problem/difference from REST is that in a GraphQL api the n+1 problem occurs on the server rather then the client. </br>
</br>
The problem with the GraphQL backend is that field resolvers are atomic and do not have any knowledge about the query as a whole. So, a field resolver does not know that it will be called multiple times in parallel to fetch similar or equal data from the same data source. </br>
</br>
A field resolver without a data loader would get called in parallel multiple times in a fetch of multiple items. </br>
This request would cause multiple requests to our data source resulting in sluggish performance and unnecessary round-trips to our data source


## Decision

We are going to use a [Data loader](https://chillicream.com/docs/hotchocolate/v15/fetching-data/dataloader) architecture in this case.
</br>
With GraphQL we have technically reduced the number of round-trips from our client to our server.
With data loaders we can centralize the data fetching and reduce the number of round trips between the data sources and the service layer.
Instead of fetching the data from the repository directly, we fetch the data from the data loader. The data loader batches all the requests together into one request to the database.

## Consequences

The benefits of the GraphQL + Data loader setup are:
- We only have to deal with the n+1 problem once on the server rather than on every client.
- Data loaders can efficiently fetch the data from the data sources.


## Additional resources
* [Data loader documentation](https://chillicream.com/docs/hotchocolate/v15/fetching-data/dataloader)
* [General overview](https://youtu.be/gVIxde5nlWE?si=pYRUm6e5ovfFbwpM)
* [Green donut overview](https://youtu.be/FhNK7KMAnXc?si=u_cTZZa2rdI0QuIF)