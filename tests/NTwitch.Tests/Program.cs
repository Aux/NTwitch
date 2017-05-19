﻿using NTwitch.Rest;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Tests
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().Start().GetAwaiter().GetResult();

        private TwitchRestClient _client;

        public async Task Start()
        {
            _client = new TwitchRestClient(new TwitchRestConfig()
            {
                LogLevel = LogLevel.Debug
            });

            _client.Log += OnLogAsync;

            await _client.LoginAsync("");
            var users = await _client.GetUsersAsync("wraxu");

            var stream = await _client.GetStreamsAsync(users.First().Id);
            
            await Task.Delay(-1);
        }

        private Task OnLogAsync(LogMessage msg)
        {
            return Console.Out.WriteLineAsync(msg.ToString());
        }
    }
}