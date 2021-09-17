using CosmosConflictResolution.Database;
using CosmosConflictResolution.Models;
using CosmosConflictResolution.Providers;
using Microsoft.Azure.Cosmos;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CosmosConflictResolution
{
    class Program
    {
        private static EtagDbService _database;
        private static RandomDataProvider _generator;
        private static Config _config;

        static async Task Main(string[] args)
        {
            _config = await ConfigurationProvider.Load("secrets.json");
            _database = new EtagDbService(_config.ConnectionString, _config.DatabaseName, _config.ContainerName);
            _generator = new RandomDataProvider();


            var item = _generator.GenerateItem();
            var id = item.Name;

            await _database.InsertAsync(item);

            try
            {
                await _database.InsertAsync(item);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                Console.WriteLine($"Failed to insert item {item.Name}, item with that ID already exists");
                var newItem = await ReadAndMutate(item.Id);
                await _database.UpsertAsync(newItem);
            }

            var itemOne = await ReadAndMutate(id);
            var itemTwo = await ReadAndMutate(id);

            await _database.UpsertAsync(itemOne);

            try
            {
                await _database.UpsertAsync(itemTwo);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.PreconditionFailed)
            {
                Console.WriteLine($"Failed to upsert item {itemTwo.Name}, etag {itemTwo.Etag} did not match");
                itemTwo = await ReadAndMutate(id);
                await _database.UpsertAsync(itemTwo);
            }
        }

        static async Task<EtagModel> ReadAndMutate(string id)
        {
            var item = await _database.ReadItem(id);
            item.Description = $"Item with etag {item.Etag} updated at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.ffffff}";
            Console.WriteLine(item.Description);
            return item;
        }

    }
}
