using Jil;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SlackSharp.Serialization.Jil
{
    /// <summary>
    /// A class that implements <see cref="IHttpContentJsonSerializer"/> with Jil.
    /// </summary>
    public class JilSerializer : IHttpContentJsonSerializer
    {
        private readonly Options _options;

        /// <summary>
        /// Create a new instance.
        /// </summary>
        public JilSerializer()
            : this(Options.Default)
        {
        }

        /// <summary>
        /// Create a new instance with recieving the configuration of Jil.
        /// </summary>
        /// <param name="settings">The configuration of Jil.</param>
        public JilSerializer(Options options)
            => _options = options ?? throw new ArgumentNullException(nameof(options));

        /// <inheritdoc cref="IHttpContentJsonSerializer.Serialize{T}(T)" />
        public HttpContent Serialize<T>(T value)
        {
            var json = JSON.Serialize(value, _options);

            return new StringContent(json);
        }

        /// <inheritdoc cref="IHttpContentJsonSerializer.Deserialize{T}(HttpContent)" />
        public async Task<T> Deserialize<T>(HttpContent content)
        {
            var json = await content.ReadAsStringAsync().ConfigureAwait(false);

            return JSON.Deserialize<T>(json, _options);
        }
    }
}
