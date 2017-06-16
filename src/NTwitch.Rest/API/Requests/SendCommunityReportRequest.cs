using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class SendCommunityReportRequest : RestRequestBuilder
    {
        public SendCommunityReportRequest(string communityId, ulong channelId)
            : base("PUT", $"communities/{communityId}/report_channel")
        {
            Parameters.Add("channel_id", channelId);
        }
    }
}
