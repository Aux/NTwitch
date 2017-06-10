using NTwitch.Pubsub.API;
using System;
using System.Collections.Generic;

namespace NTwitch.Pubsub.Queue
{
    public class PubsubRequestBuilder
    {
        public List<string> Topics { get; }

        private readonly string _nonce;
        private readonly string _authToken;
        private string _defaultType;

        public PubsubRequestBuilder(string type, string authToken = null)
        {
            Topics = new List<string>();
            _nonce = new Guid().ToString();
            _authToken = authToken;
            _defaultType = type;
        }

        public object GetPayload(bool includeNonce = false)
        {
            var payload = new PubsubFrame<PubsubOutData>
            {
                Type = _defaultType,
                Data = new PubsubOutData
                {
                    Topics = Topics
                }
            };

            if (_authToken != null)
                payload.Data.AuthToken = _authToken;
            if (includeNonce)
                payload.Nonce = _nonce;
            return payload;
        }

        public string GetNonce()
            => _nonce;
    }
}
