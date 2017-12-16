﻿using NTwitch.Helix.Rest;
using System.Threading.Tasks;

namespace NTwitch.Helix.Queue
{
    public class JsonRestRequest : RestRequest
    {
        public string Json { get; }

        public JsonRestRequest(IRestClient client, string method, string endpoint, string json, RequestOptions options)
            : base(client, method, endpoint, options)
        {
            Json = json;
        }

        public override async Task<RestResponse> SendAsync()
        {
            return await Client.SendAsync(Method, Endpoint, Json, Options.CancelToken, Options.HeaderOnly).ConfigureAwait(false);
        }
    }
}
