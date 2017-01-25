using System;

namespace NTwitch.Chat.API
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class ChatPropertyAttribute : Attribute
    {
        public string Name;

        public ChatPropertyAttribute(string name = null)
        {
            Name = name;
        }
    }
}
