using System;

namespace NTwitch.Rest
{
    public class MissingScopeException : ArgumentException
    {
        public MissingScopeException(params string[] scopes)
            : base($"Missing required scopes: {string.Join(", ", scopes)}") { }
    }
}
