using System;
using System.Collections.Generic;
using System.Linq;

namespace NTwitch.Chat
{
    internal class TwitchMessage
    {
        public Dictionary<string, string> Tags { get; private set; } = new Dictionary<string, string>();
        public string Prefix { get; private set; }
        public string Command { get; private set; }
        public List<string> Parameters { get; private set; } = new List<string>();

        public bool IsUser()
            => Prefix.Contains('!') && Prefix.Contains('@');
        
        public static TwitchMessage Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new FormatException("Input string is empty.");

            var msg = new TwitchMessage();
            int pos = 0;
            int next;

            // Parse tags
            if (input[0] == '@')
            {
                next = input.IndexOf(' ');
                if (next < 0)
                    throw new FormatException();

                string tagPart = input.Substring(1, next - 1);
                var rawTags = tagPart.Split(';');

                foreach (var tag in rawTags.Select(x => x.Split('=')))
                {
                    string key = tag[0];
                    string value = tag[1] == "" ? null : tag[1];

                    msg.Tags.Add(key, value);
                }

                pos = next + 1;
            }

            // Parse prefix
            if (input[pos] == ':')
            {
                next = input.IndexOf(' ', pos);
                if (next < 0)
                    throw new FormatException();

                string prefixPart = input.Substring(pos + 1, next - pos - 1);
                msg.Prefix = prefixPart;

                pos = next + 1;
            }

            // Check for command and parameters
            next = input.IndexOf(' ', pos);
            if (next < 0)
            {
                string cmdPart = input.Substring(pos);
                msg.Command = cmdPart;
            } else
            {
                string cmdPart = input.Substring(pos, next - pos);
                msg.Command = cmdPart;

                pos = next + 1;
                while (pos < input.Length)
                {
                    if (input[pos] == ':')
                    {
                        string contentPart = input.Substring(pos + 1);
                        msg.Parameters.Add(contentPart);
                        break;
                    }

                    next = input.IndexOf(' ', pos);

                    if (next > -1)
                    {
                        string paramPart = input.Substring(pos, next - pos);
                        msg.Parameters.Add(paramPart);

                        pos = next + 1;
                    }
                }
            }
            
            return msg;
        }

        public override string ToString()
            => $"{Command} {string.Join(" ", Parameters)}";
    }
}
