namespace NTwitch.Rest
{
    internal class CommunityReportRequest : RestRequest
    {
        public CommunityReportRequest(string id, ulong channelId) 
            : base("GET", $"communities/{id}/report_channel")
        {
            Parameters.Add("channel_id", channelId);
        }
    }
}
