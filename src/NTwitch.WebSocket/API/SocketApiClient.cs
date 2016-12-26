using System;

namespace NTwitch.WebSocket
{
    internal class SocketApiClient : IDisposable
    {
        private LogManager _log;
        private SocketState _state;
        private string _baseurl;
        private string _token;
        private int _port;

        public SocketApiClient(LogManager manager, string baseurl, int port = 11000)
        {
            _log = manager;
            _baseurl = baseurl;
            _port = port;
        }

        public void Dispose()
        {
            _state.Dispose();
        }
    }
}