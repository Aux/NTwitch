namespace NTwitch.Rest
{
    internal class GetStreamSummaryRequest : RestRequest
    {
        public GetStreamSummaryRequest(string game) 
            : base("GET", "streams/summary")
        {
            Parameters.Add("game", game);
        }
    }
}
