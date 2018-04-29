using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Utf8Json;
using Utf8Json.Resolvers;

namespace SlackSharp.Serialization.Utf8Json
{
    /// <summary>
    /// A class that implements <see cref="IHttpContentJsonSerializer"/> with Utf8Json.
    /// </summary>
    public class Utf8JsonSerializer : IHttpContentJsonSerializer
    {
        private readonly IJsonFormatterResolver _resolver;

        /// <summary>
        /// Create a new instance.
        /// </summary>
        public Utf8JsonSerializer()
            : this(StandardResolver.Default)
        {
        }

        /// <summary>
        /// Create a new instance with recieving the JSON fomat resolver of Utf8Json.
        /// </summary>
        /// <param name="resolver">The JSON fomat resolver of Utf8Json.</param>
        public Utf8JsonSerializer(IJsonFormatterResolver resolver)
            => _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));

        /// <inheritdoc cref="IHttpContentJsonSerializer.Serialize{T}(T)" />
        public HttpContent Serialize<T>(T value)
        {
            var json = JsonSerializer.Serialize(value, _resolver);

            var content = new ByteArrayContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return content;
        }

        /// <inheritdoc cref="IHttpContentJsonSerializer.Deserialize{T}(HttpContent)" />
        public async Task<T> Deserialize<T>(HttpContent content)
        {
            var json = await content.ReadAsByteArrayAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<T>(json, _resolver);
        }
    }
}