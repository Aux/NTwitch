namespace NTwitch.Rest
{
    internal class GetFeaturedStreamsRequest : OldRestRequest
    {
        public GetFeaturedStreamsRequest(string token, uint limit, uint offset)
            : base("GET", "streams/featured", token)
        {
            Parameters.Add("limit", limit);
            Parameters.Add("offset", offset);
        }
    }
}
