using CosmosConflictResolution.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace CosmosConflictResolution.Providers
{
    public class ConfigurationProvider
    {
        public static async Task<Config> Load(string filePath)
        {
            var fs = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<Config>(fs);
        }
    }
}
