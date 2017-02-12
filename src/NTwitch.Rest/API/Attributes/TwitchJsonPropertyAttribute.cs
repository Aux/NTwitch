using System;

namespace NTwitch
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TwitchJsonPropertyAttribute : Attribute
    {
        public string[] Aliases { get; }
        
        public TwitchJsonPropertyAttribute(params string[] aliases)
        {
            Aliases = aliases;
        }
    }
}
