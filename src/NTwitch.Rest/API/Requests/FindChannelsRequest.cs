using NTwitch.Rest.Queue;
using System;

namespace NTwitch.Rest.API
{
    public class FindChannelsRequest : RestRequestBuilder
    {
        public FindChannelsRequest(string query, PageOptions paging) 
            : base("GET", "search/channels")
        {
            Parameters.Add("query", Uri.EscapeDataString(query));
            Parameters.Add("limit", paging.Limit);
            Parameters.Add("offset", paging.Offset);
        }
    }
}
