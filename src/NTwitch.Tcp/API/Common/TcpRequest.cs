namespace NTwitch.Tcp
{
    public class TcpRequest
    {
        public string Command { get; }
        public string Parameters { get; }

        public TcpRequest(string command, string parameters)
        {
            Command = command;
            Parameters = parameters;
        }

        public override string ToString()
            => $"{Command} {Parameters}";
    }
}
