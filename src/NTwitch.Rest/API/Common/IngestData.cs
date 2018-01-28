using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class IngestData
    {
        [JsonProperty("ingests")]
        public IReadOnlyCollection<Ingest> Ingests { get; set; }
    }
}
