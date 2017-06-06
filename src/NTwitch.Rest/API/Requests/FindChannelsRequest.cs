using NTwitch.Rest.Queue;
using System;

namespace NTwitch.Rest.API
{
    public class FindChannelsRequest : RequestBuilder
    {
        public FindChannelsRequest(string query, PageOptions paging) 
            : base("GET", "search/channels")
        {
            _endpointParams.Add("query", Uri.EscapeDataString(query));
            _endpointParams.Add("limit", paging.Limit);
            _endpointParams.Add("offset", paging.Offset);
        }
    }
}
