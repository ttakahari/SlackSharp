using SlackSharp.Models;
using System;
using System.Text;
using System.Threading.Tasks;
using Utf8Json.Resolvers;
using Xunit;

namespace SlackSharp.Serialization.Utf8Json.Tests
{
    public class Utf8JsonSerializerTests
    {
        private const string WebHookUrl = @"";

        [Fact]
        public void Constructor_Tests()
        {
            {
                Assert.Throws<ArgumentNullException>(() => new Utf8JsonSerializer(null));
            }

            {
                var serializer = new Utf8JsonSerializer();

                Assert.NotNull(serializer);
            }

            {
                var serializer = new Utf8JsonSerializer(StandardResolver.Default);

                Assert.NotNull(serializer);
            }
        }

        [Fact]
        public async Task Serialize_Tests()
        {
            using (var client = new WebHookClient(new Utf8JsonSerializer()))
            {
                {
                    var message = new StringBuilder("utf8json simple message test.")
#if NETCOREAPP2_0
                        .Append("netcoreapp2.0.")
#elif NET46
                        .Append("net46.")
#endif
                        .ToString();

                    var result = await client.SendAsync(WebHookUrl, message);
                }

                {
                    var message = new StringBuilder("utf8json structured message test.")
#if NETCOREAPP2_0
                        .Append("netcoreapp2.0.")
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
