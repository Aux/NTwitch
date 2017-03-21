using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class ChatProperties
    {
        public ulong Id { get; set; }
        public bool HideChatLinks { get; set; }
        public int ChatDelayDuration { get; set; }
        public string ChatRules { get; set; }
        public ulong TwitchbotRuleId { get; set; }
        public bool DevChat { get; set; }
        public string Game { get; set; }
        public bool RequireVerified { get; set; }
        public bool IsSubsOnly { get; set; }
        public IEnumerable<string> IrcServers { get; set; }
        public IEnumerable<string> WsServers { get; set; }
        public int WebsocketPort { get; set; }
        public int DarklaunchPort { get; set; }
        public string BlockChatNotificationToken { get; set; }
        public string[] AvailableChatNotificationTokens { get; set; }
        public string SceTitlePresetText1 { get; set; }
        public string SceTitlePresetText2 { get; set; }
        public string SceTitlePresetText3 { get; set; }
        public string SceTitlePresetText4 { get; set; }
        public string SceTitlePresetText5 { get; set; }
    }
}
