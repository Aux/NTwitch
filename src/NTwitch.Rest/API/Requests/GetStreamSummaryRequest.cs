namespace NTwitch.Rest
{
    internal class GetStreamSummaryRequest : OldRestRequest
    {
        public GetStreamSummaryRequest(string token, string game) 
            : base("GET", "streams/summary", token)
        {
            Parameters.Add("game", game);
        }
    }
}
