# Cosmos Conflict Resolution

## Problem

**How do we handle write collisions when using Azure Cosmos DB?**

## Solution

**tl;dr: \_etag for the win**

The solution I've researched and tested is based on a system defined field on every Cosmos DB resource, the Entity Tag, named `_etag` in the actual document. This is a unique value that is updated upon insertions and updates to any given record. The value is in the format `"00000000-0000-0000-0000-000000000000"`, where the empty GUID is a stand-in for any GUID and including the quotes.

PUT or DELETE requests to the Cosmos DB can then contain a header named `If-Match`, where the value is the `_etag` of the resource that was read from the database. If this value does not match the current value stored in the database, a failure response will be sent with status code `412 Precondition Failure`, indicating that the record should be re-read before retrying.

With this in mind, a consideration to be made when using the `_etag` for optimistic concurrency handling, is that there needs to be a matching field in the data model for the serialized resources read from Cosmos, as otherwise the tag cannot be passed to the request headers, however your SDK of choice chooses to do that.

## Code

### Cosmos SDK v3.x

```C#
await cosmosContainer.UpsertItemAsync(data, new PartitionKey(data.Name), new ItemRequestOptions
{
    IfMatchEtag = data.Etag
});
```

In the above case, the etag is passed to the `IfMatchEtag` field of the `ItemRequestOptions` object used for the request. The SDK then handles adding the `If-Match` header to the request.

`IfMatchEtag` is inherited into `ItemRequestOptions` from `Microsoft.Azure.Cosmos.RequestOptions`.

## Further Reading

[MS Docs: RequestOptions.IfMatchEtag](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.cosmos.requestoptions.ifmatchetag?view=azure-dotnet)

[Understanding Optimistic Concurrency](https://chapsas.com/understanding-optimistic-concurrency-in-cosmos-db/)

[HTTP Status Codes for Azure Cosmos DB](https://docs.microsoft.com/en-us/rest/api/cosmos-db/http-status-codes-for-cosmosdb)
