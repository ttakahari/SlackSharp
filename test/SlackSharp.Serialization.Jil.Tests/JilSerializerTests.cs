using Jil;
using SlackSharp.Models;
using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SlackSharp.Serialization.Jil.Tests
{
    public class JilSerializerTests
    {
        private const string WebHookUrl = @"";

        [Fact]
        public void Constructor_Tests()
        {
            {
                Assert.Throws<ArgumentNullException>(() => new JilSerializer(null));
            }

            {
                var serializer = new JilSerializer();

                Assert.NotNull(serializer);
            }

            {
                var serializer = new JilSerializer(Options.Default);

                Assert.NotNull(serializer);
            }
        }

        [Fact]
        public async Task Serialize_Tests()
        {
            using (var client = new WebHookClient(new JilSerializer()))
            {
                {
                    var message = new StringBuilder("jil simple message test.")
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
                    var message = new StringBuilder("jil structured message test.")
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
