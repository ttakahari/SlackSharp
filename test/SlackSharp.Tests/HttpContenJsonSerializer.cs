using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace SlackSharp.Tests
{
    public class HttpContenJsonSerializer : IHttpContentJsonSerializer
    {
        private static readonly ConcurrentDictionary<Type, DataContractJsonSerializer> _serializers = new ConcurrentDictionary<Type, DataContractJsonSerializer>();

        public HttpContent Serialize<T>(T value)
        {
            var serialier = _serializers.GetOrAdd(typeof(T), type => new DataContractJsonSerializer(type));

            byte[] json;

            using (var stream = new MemoryStream())
            {
                serialier.WriteObject(stream, value);

                json = stream.ToArray();
            }

            var content = new ByteArrayContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return content;
        }

        public async Task<T> Deserialize<T>(HttpContent content)
        {
            var serialier = _serializers.GetOrAdd(typeof(T), type => new DataContractJsonSerializer(type));
            var json = await content.ReadAsByteArrayAsync().ConfigureAwait(false);

            using (var stream = new MemoryStream(json))
            {
                return (T)serialier.ReadObject(stream);
            }
        }
    }
}
