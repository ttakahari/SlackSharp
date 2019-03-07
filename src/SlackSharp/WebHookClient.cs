using JsonHttpContentConverter;
using SlackSharp.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SlackSharp
{
    /// <summary>
    /// A client class that has methods to send messages with Incoming WebHooks.
    /// </summary>
    public class WebHookClient : IWebHookClient
    {
        private bool _disposed;

        private readonly HttpClient _client;
        private readonly IJsonHttpContentConverter _serializer;

        /// <summary>
        /// Create a new instance with recieveing an instance of <see cref="IJsonHttpContentConverter"/>.
        /// </summary>
        /// <param name="serializer">The instance of <see cref="IJsonHttpContentConverter"/>.</param>
        public WebHookClient(IJsonHttpContentConverter serializer)
            : this(serializer, new HttpClientHandler(), true)
        {
        }

        /// <summary>
        /// Create a new instance with recieveing an instance of <see cref="IJsonHttpContentConverter"/> and an inner handler.
        /// </summary>
        /// <param name="serializer">instance of <see cref="IJsonHttpContentConverter"/>.</param>
        /// <param name="handler">The inner handler.</param>
        public WebHookClient(IJsonHttpContentConverter serializer, HttpMessageHandler handler)
            : this(serializer, handler, true)
        {
        }

        /// <summary>
        /// Create a new instance with recieveing an instance of <see cref="IJsonHttpContentConverter"/>, an inner handler and whether disposig the inner handler or not.
        /// </summary>
        /// <param name="serializer">instance of <see cref="IJsonHttpContentConverter"/>.</param>
        /// <param name="handler">The inner handler.</param>
        /// <param name="disposeHandler">Whether disposing the inner handler or not.</param>
        public WebHookClient(IJsonHttpContentConverter serializer, HttpMessageHandler handler, bool disposeHandler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _client = new HttpClient(handler, disposeHandler);
        }

        /// <inheritdoc cref="IWebHookClient.SendAsync(string, string)" />
        public async Task<ResponseMessage> SendAsync(string url, string message)
            => await SendAsync(url, new Payload { Text = message }).ConfigureAwait(false);

        /// <inheritdoc cref="IWebHookClient.SendAsync(string, Payload)" />
        public async Task<ResponseMessage> SendAsync(string url, Payload payload)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));
            if (payload == null) throw new ArgumentNullException(nameof(payload));

            var content = _serializer.ToJsonHttpContent(payload);
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

        /// <summary>
        /// A destructor for disposing this instance.
        /// </summary>
        ~WebHookClient()
            => Dispose(false);

        /// <inheritdoc cref="IDisposable.Dispose()" />
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
