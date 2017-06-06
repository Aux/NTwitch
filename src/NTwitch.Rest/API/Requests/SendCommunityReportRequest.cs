using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class SendCommunityReportRequest : RequestBuilder
    {
        public SendCommunityReportRequest(string communityId, ulong channelId)
            : base("PUT", $"communities/{communityId}/report_channel")
        {
            _endpointParams.Add("channel_id", channelId);
        }
    }
}
