using NTwitch.Chat.Queue;

namespace NTwitch.Chat.API
{
    public class ClearChatRequest : ChatRequestBuilder
    {
        public ClearChatRequest(string channelName, string userName, string reason = null, uint? duration = null) 
            : base("CLEARCHAT", $"#{channelName} :{userName}")
        {
            if (reason != null)
                Tags.Add("ban-reason", reason);
            if (duration != null)
                Tags.Add("ban-duration", duration);
        }
    }
}
