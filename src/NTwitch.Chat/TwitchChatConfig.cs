﻿using NTwitch.Rest;

namespace NTwitch.Chat
{
    public class TwitchChatConfig : TwitchRestConfig
    {
        /// <summary> Allow the authenticated user to speak in channels without the moderator permission. </summary>
        public bool CanSpeakWithoutMod { get; set; } = false;
        public string ChatHost { get; set; } = "irc.chat.twitch.tv";
        public int ChatPort { get; set; } = 6667;
    }
}
