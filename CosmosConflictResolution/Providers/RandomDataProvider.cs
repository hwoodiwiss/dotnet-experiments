using CosmosConflictResolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosConflictResolution.Providers
{
    public class RandomDataProvider
    {

        public EtagModel GenerateItem()
        {
            var random = new Random();
            var bytes = new byte[100];
            random.NextBytes(bytes);
            //Bound the random bytes to ASCII printable chars
            var boundedBytes = bytes.Select(b => (byte)((b % 62) + 64)).ToArray();

            return new EtagModel
            {
                Name = Guid.NewGuid().ToString(),
                Age = random.Next(0, 100),
                Description = $"Generated data created: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.ffffff}",
                Data = Encoding.UTF8.GetString(boundedBytes)
            };
        }
    }
}
