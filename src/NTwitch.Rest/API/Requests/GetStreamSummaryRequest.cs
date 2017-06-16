using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetStreamSummaryRequest : RestRequestBuilder
    {
        public GetStreamSummaryRequest(string game) 
            : base("GET", "streams/summary")
        {
            Parameters.Add("game", game);
        }
    }
}
