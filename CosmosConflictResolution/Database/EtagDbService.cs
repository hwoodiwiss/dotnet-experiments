using CosmosConflictResolution.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosConflictResolution.Database
{
    public class EtagDbService
    {
        CosmosClient _cosmosClient;
        Container _container;

        public EtagDbService(string connectionString, string dbName, string containerName)
        {
            _cosmosClient = new CosmosClient(connectionString);
            _container = _cosmosClient.GetContainer(dbName, containerName);
        }

        public Task UpsertAsync(EtagModel data)
        {
            Console.WriteLine($"Upserting object with etag {data.Etag} at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.ffffff}");
            return _container.UpsertItemAsync(data, new PartitionKey(data.Name), new ItemRequestOptions
            {
                IfMatchEtag = data.Etag
            });
        }

        public async Task<EtagModel> ReadItem(string id)
        {
            var response = await _container.ReadItemAsync<EtagModel>(id, new PartitionKey(id));

            return response.Resource;
        }
    }
}
