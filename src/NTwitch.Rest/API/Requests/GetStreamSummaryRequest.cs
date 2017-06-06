using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetStreamSummaryRequest : RequestBuilder
    {
        public GetStreamSummaryRequest(string game) 
            : base("GET", "streams/summary")
        {
            _endpointParams.Add("game", game);
        }
    }
}
