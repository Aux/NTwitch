using NTwitch.Rest.Queue;
using System;

namespace NTwitch.Rest.API
{
    public class FindStreamsRequest : RequestBuilder
    {
        public FindStreamsRequest(string query, bool? hls, PageOptions paging) 
            : base("GET", "search/streams")
        {
            _endpointParams.Add("query", Uri.EscapeDataString(query));
            _endpointParams.Add("limit", paging.Limit);
            _endpointParams.Add("offset", paging.Offset);
            if (hls != null)
                _endpointParams.Add("hls", hls);
        }
    }
}
