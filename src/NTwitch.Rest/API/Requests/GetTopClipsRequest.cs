namespace NTwitch.Rest.API
{
    internal class GetTopClipsRequest : OldRestRequest
    {
        public GetTopClipsRequest(string token, TopClipsParams options)
            : base("GET", "clips/top", token)
        {
            Parameters.Add("limit", options.Limit);
            Parameters.Add("trending", options.IsTrending);
            Parameters.Add("period", options.Period.ToLower());
            Parameters.Add("channel", string.Join(",", options.Channels));
            Parameters.Add("game", string.Join(",", options.Games));
        }
    }
}
