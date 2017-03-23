using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class IngestCollection
    {
        [JsonProperty("ingests")]
        public IEnumerable<Ingest> Ingests { get; set; }
    }
}
