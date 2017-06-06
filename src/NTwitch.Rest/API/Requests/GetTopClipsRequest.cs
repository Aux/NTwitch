using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetTopClipsRequest : RequestBuilder
    {
        public GetTopClipsRequest(TopClipsParams parameters)
            : base("GET", "clips/top")
        {
            _endpointParams.Add("trending", parameters.IsTrending);
            _endpointParams.Add("period", parameters.Period.ToLower());
            _endpointParams.Add("channel", string.Join(",", parameters.Channels));
            _endpointParams.Add("game", string.Join(",", parameters.Games));
        }
    }
}
