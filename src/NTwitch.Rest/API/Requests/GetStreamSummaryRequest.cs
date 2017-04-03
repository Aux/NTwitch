namespace NTwitch.Rest
{
    internal class GetStreamSummaryRequest : RestRequest
    {
        public GetStreamSummaryRequest(string token, string game) 
            : base("GET", "streams/summary", token)
        {
            Parameters.Add("game", game);
        }
    }
}
