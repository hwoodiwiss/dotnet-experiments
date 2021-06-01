using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosConflictResolution.Models
{
    public class EtagModel
    {
        [JsonProperty("id")] public string Id => Name;

        [JsonProperty("name")] 
        public string Name { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }        
        
        [JsonProperty("data")]
        public string Data { get; set; }        
        
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("_etag")]
        public string Etag { get; set; }
    }
}
