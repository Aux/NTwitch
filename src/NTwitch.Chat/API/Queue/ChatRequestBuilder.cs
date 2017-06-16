using System.Collections.Generic;

namespace NTwitch.Chat.Queue
{
    public class ChatRequestBuilder
    {
        public string Command => _defaultCommand;
        public string Parameters => _defaultParameters;
        public string Message => ToString();

        public Dictionary<string, object> Tags { get; protected set; }

        internal string _defaultCommand;
        internal string _defaultParameters;

        public ChatRequestBuilder(string command, string parameters)
        {
            _defaultCommand = command;
            _defaultParameters = parameters;
            Tags = new Dictionary<string, object>();
        }

        public string GetTagString()
        {
            if (Tags.Count == 0)
                return "";

            List<string> paramList = new List<string>();
            foreach (var p in Tags)
                if (p.Value != null) paramList.Add($"{p.Key}={p.Value}");
            return $"@{string.Join(";", paramList.ToArray())} ";
        }

        public override string ToString()
            => GetTagString() + _defaultCommand + _defaultParameters;
    }
}
