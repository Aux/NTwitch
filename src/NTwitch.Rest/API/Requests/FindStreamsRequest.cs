using NTwitch.Rest.Queue;
using System;

namespace NTwitch.Rest.API
{
    public class FindStreamsRequest : RestRequestBuilder
    {
        public FindStreamsRequest(string query, bool? hls, PageOptions paging) 
            : base("GET", "search/streams")
        {
            Parameters.Add("query", Uri.EscapeDataString(query));
            Parameters.Add("limit", paging.Limit);
            Parameters.Add("offset", paging.Offset);
            if (hls != null)
                Parameters.Add("hls", hls);
        }
    }
}
