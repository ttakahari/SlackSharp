using Newtonsoft.Json;
using SlackSharp.Models;
using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SlackSharp.Serialization.JsonNet.Tests
{
    public class JsonNetSerializerTests
    {
        private const string WebHookUrl = @"";

        [Fact]
        public void Constructor_Tests()
        {
            {
                Assert.Throws<ArgumentNullException>(() => new JsonNetSerializer(null));
            }

            {
                var serializer = new JsonNetSerializer();

                Assert.NotNull(serializer);
            }

            {
                var serializer = new JsonNetSerializer(new JsonSerializerSettings());

                Assert.NotNull(serializer);
            }
        }

        [Fact]
        public async Task Serialize_Tests()
        {
            using (var client = new WebHookClient(new JsonNetSerializer()))
            {
                {
                    var message = new StringBuilder("json.net simple message test.")
#if NETCOREAPP2_0
                        .Append("netcoreapp2.0.")
#elif NETCOREAPP1_1
                        .Append("netcoreapp1.1.")
#elif NETCOREAPP1_0
                        .Append("netcoreapp1.0.")
#elif NET46
                        .Append("net46.")
#endif
                        .ToString();

                    var result = await client.SendAsync(WebHookUrl, message);
                }

                {
                    var message = new StringBuilder("json.net structured message test.")
#if NETCOREAPP2_0
                        .Append("netcoreapp2.0.")
#elif NETCOREAPP1_1
                        .Append("netcoreapp1.1.")
#elif NETCOREAPP1_0
                        .Append("netcoreapp1.0.")
#elif NET46
                        .Append("net46.")
#endif
                        .ToString();

                    var result = await client.SendAsync(WebHookUrl, new Payload { Text = message });
                }
            }
        }
    }
}
