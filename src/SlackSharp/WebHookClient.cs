﻿using SlackSharp.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SlackSharp
{
    public class WebHookClient : IWebHookClient
    {
        private bool _disposed;

        private readonly HttpClient _client;
        private readonly IHttpContentJsonSerializer _serializer;

        public WebHookClient(IHttpContentJsonSerializer serializer)
            : this(serializer, new HttpClientHandler(), true)
        {
        }

        public WebHookClient(IHttpContentJsonSerializer serializer, HttpMessageHandler handler)
            : this(serializer, handler, true)
        {
        }

        public WebHookClient(IHttpContentJsonSerializer serializer, HttpMessageHandler handler, bool disposeHandler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _client = new HttpClient(handler, disposeHandler);
        }

        public async Task<ResponseMessage> SendAsync(string url, string message)
            => await SendAsync(url, new Payload { Text = message }).ConfigureAwait(false);

        public async Task<ResponseMessage> SendAsync(string url, Payload payload)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));
            if (payload == null) throw new ArgumentNullException(nameof(payload));

            var content = _serializer.Serialize(payload);
            var response = await _client.PostAsync(url, content).ConfigureAwait(false);
            var message = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            switch (message)
            {
                case "ok":
                    return ResponseMessage.Ok;
                case "noteam":
                    return ResponseMessage.NoTeam;
                case "noservice":
                    return ResponseMessage.NoService;
                default:
                    return ResponseMessage.None;
            }
        }

        ~WebHookClient()
            => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _client.Dispose();
            }

            _disposed = true;
        }
    }
}
