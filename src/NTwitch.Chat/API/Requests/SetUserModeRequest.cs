using NTwitch.Chat.Queue;

namespace NTwitch.Chat.API
{
    public class SetUserModeRequest : ChatRequestBuilder
    {
        public SetUserModeRequest(string channelName, string userName, bool isOp) 
            : base("MODE", null)
        {
            string status = isOp ? "+o" : "-o";
            _defaultCommand = $"#{channelName} {status} {userName}";
        }
    }
}
