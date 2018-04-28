using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SlackSharp.Serialization.JsonNet
{
    /// <summary>
    /// A class that implements <see cref="IHttpContentJsonSerializer"/> with Json.NET.
    /// </summary>
    public class JsonNetSerializer : IHttpContentJsonSerializer
    {
        private readonly JsonSerializerSettings _settings;

        /// <summary>
        /// Create a new instance.
        /// </summary>
        public JsonNetSerializer()
            : this(new JsonSerializerSettings())
        {
        }

        /// <summary>
        /// Create a new instance with recieving the settings of Json.NET.
        /// </summary>
        /// <param name="settings">The settings of Json.NET.</param>
        public JsonNetSerializer(JsonSerializerSettings settings)
            => _settings = settings ?? throw new ArgumentNullException(nameof(settings));

        /// <inheritdoc cref="IHttpContentJsonSerializer.Serialize{T}(T)" />
        public HttpContent Serialize<T>(T value)
        {
            var json = JsonConvert.SerializeObject(value, _settings);

            return new StringContent(json);
        }

        /// <inheritdoc cref="IHttpContentJsonSerializer.Deserialize{T}(HttpContent)" />
        public async Task<T> Deserialize<T>(HttpContent content)
        {
            var json = await content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(json, _settings);
        }
    }
}
