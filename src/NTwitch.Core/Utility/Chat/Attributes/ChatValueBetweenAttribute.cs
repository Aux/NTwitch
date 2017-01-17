using System;

namespace NTwitch
{
    public class ChatValueBetweenAttribute : Attribute
    {
        public string FromValue;
        public string ToValue;

        public ChatValueBetweenAttribute(string from, string to)
        {
            FromValue = from;
            ToValue = to;
        }
    }
}
