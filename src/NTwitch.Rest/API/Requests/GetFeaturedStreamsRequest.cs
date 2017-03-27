namespace NTwitch.Rest
{
    internal class GetFeaturedStreamsRequest : RestRequest
    {
        public GetFeaturedStreamsRequest(uint limit, uint offset)
            : base("GET", "streams/featured")
        {
            Parameters.Add("limit", limit);
            Parameters.Add("offset", offset);
        }
    }
}
