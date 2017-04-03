namespace NTwitch.Rest
{
    internal class GetFeaturedStreamsRequest : RestRequest
    {
        public GetFeaturedStreamsRequest(string token, uint limit, uint offset)
            : base("GET", "streams/featured", token)
        {
            Parameters.Add("limit", limit);
            Parameters.Add("offset", offset);
        }
    }
}
