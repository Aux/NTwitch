#pragma warning disable CS1998
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    internal class SocketClient : IDisposable
    {
        private LogManager _log;
        private ClientWebSocket _client;
        private CancellationTokenSource _cancelTokenSource;
        private ConcurrentDictionary<string, Func<PubsubResponse, Task>> _callbacks;
        private Task _task;

        private string _host;
        private string _token;
        private bool _isFinal = false;
        private bool _disposed = false;

        private readonly AsyncEvent<Func<PubsubMessage, Task>> _eventReceivedEvent = new AsyncEvent<Func<PubsubMessage, Task>>();
        internal event Func<PubsubMessage, Task> EventReceived
        {
            add { _eventReceivedEvent.Add(value); }
            remove { _eventReceivedEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<string, Task>> _messageReceivedEvent = new AsyncEvent<Func<string, Task>>();
        private event Func<string, Task> MessageReceivedInternal
        {
            add { _messageReceivedEvent.Add(value); }
            remove { _messageReceivedEvent.Remove(value); }
        }

        public SocketClient(LogManager log, string host, string token = null)
        {
            _log = log;
            _host = host;
            _token = token;
            _client = new ClientWebSocket();
            _callbacks = new ConcurrentDictionary<string, Func<PubsubResponse, Task>>();
            MessageReceivedInternal += OnMessageReceivedInternalAsync;
        }

        private Task OnMessageReceivedInternalAsync(string content)
        {
            var response = JsonConvert.DeserializeObject<PubsubResponse>(content);

            if (response == null)
            {
                var message = JsonConvert.DeserializeObject<PubsubMessage>(content);
                return _eventReceivedEvent.InvokeAsync(message);
            }

            if (_callbacks.TryGetValue(response.Nonce, out Func<PubsubResponse, Task> action))
                action.Invoke(response);

            return Task.CompletedTask;
        }

        public async Task SendAsync(string method, string topic)
        {
            if (_client.State != WebSocketState.Open)
                throw new InvalidOperationException("Client is not connected.");

            var request = new PubsubRequest()
            {
                Type = method,
                Nonce = new Guid().ToString(),
                Data = new PubsubRequestData()
                {
                    Token = _token,
                    Topics = new List<string> { topic }
                }
            };

            var content = JsonConvert.SerializeObject(request);
            var bytes = Encoding.UTF8.GetBytes(content);
            var buffer = new ArraySegment<Byte>(bytes);
            await _client.SendAsync(buffer, WebSocketMessageType.Text, _isFinal, _cancelTokenSource.Token);
            _callbacks.TryAdd(request.Nonce, ReceiveAsync);
        }

        public Task ReceiveAsync(PubsubResponse message)
        {
            if (!string.IsNullOrWhiteSpace(message.Error))
                return _log.ErrorAsync("Response", message.Error);
            else
                return Task.CompletedTask;
        }

        public async Task ConnectAsync()
        {
            _cancelTokenSource = new CancellationTokenSource();
            await _client.ConnectAsync(new Uri(_host), _cancelTokenSource.Token);
            await StartAsync();
        }

        public async Task StartAsync()
        {
            _task = RunAsync();
        }

        public async Task RunAsync()
        {
            while (_client.State == WebSocketState.Open)
            {
                try
                {
                    var buffer = new ArraySegment<Byte>(new Byte[4096]);
                    var received = await _client.ReceiveAsync(buffer, _cancelTokenSource.Token);

                    if (received.MessageType == WebSocketMessageType.Close)
                    {
                        await StopAsync();
                        return;
                    }

                    var data = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
                    await _messageReceivedEvent.InvokeAsync(data);
                }
                catch (Exception ex)
                {
                    await _log.ErrorAsync("Socket", ex).ConfigureAwait(false);
                }
            }
        }

        public Task StopAsync()
        {
            throw new NotImplementedException();
        }
        
        public Task DisconnectAsync()
        {
            throw new NotImplementedException();
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
