using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetStreamsRequest : RequestBuilder
    {
        public GetStreamsRequest(GetStreamsParams parameters, PageOptions paging)
            : base("GET", "streams")
        {
            if (parameters.ChannelIds != null)
                _endpointParams.Add("channel", string.Join(",", parameters.ChannelIds));
            if (parameters.Game != null)
                _endpointParams.Add("game", parameters.Game);
            if (parameters.Language != null)
                _endpointParams.Add("language", parameters.Language);
            
            _endpointParams.Add("type", parameters.Type.ToString().ToLower());
            _endpointParams.Add("limit", paging.Limit);
            _endpointParams.Add("offset", paging.Offset);
        }
    }
}
