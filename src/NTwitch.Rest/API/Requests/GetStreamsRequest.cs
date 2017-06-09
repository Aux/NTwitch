using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetStreamsRequest : RestRequestBuilder
    {
        public GetStreamsRequest(GetStreamsParams parameters, PageOptions paging)
            : base("GET", "streams")
        {
            if (parameters.ChannelIds != null)
                Parameters.Add("channel", string.Join(",", parameters.ChannelIds));
            if (parameters.Game != null)
                Parameters.Add("game", parameters.Game);
            if (parameters.Language != null)
                Parameters.Add("language", parameters.Language);
            
            Parameters.Add("type", parameters.Type.ToString().ToLower());
            Parameters.Add("limit", paging.Limit);
            Parameters.Add("offset", paging.Offset);
        }
    }
}
