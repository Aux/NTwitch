using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetTopClipsRequest : RestRequestBuilder
    {
        public GetTopClipsRequest(TopClipsParams parameters)
            : base("GET", "clips/top")
        {
            Parameters.Add("trending", parameters.IsTrending);
            Parameters.Add("period", parameters.Period.ToLower());
            Parameters.Add("channel", string.Join(",", parameters.Channels));
            Parameters.Add("game", string.Join(",", parameters.Games));
        }
    }
}
