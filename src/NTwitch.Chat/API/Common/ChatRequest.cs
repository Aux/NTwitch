namespace NTwitch.Chat.API
{
    public class ChatRequest
    {
        public string Command { get; }
        public string Parameters { get; }

        public ChatRequest(string command, string parameters)
        {
            Command = command;
            Parameters = parameters;
        }

        public override string ToString()
            => $"{Command} {Parameters}";
    }
}
