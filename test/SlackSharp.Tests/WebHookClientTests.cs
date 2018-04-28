using SlackSharp.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SlackSharp.Tests
{
    public class WebHookClientTests
    {
        private const string WebHookUrl = @"";

        [Fact]
        public void Constructor_Tests()
        {
            {
                Assert.Throws<ArgumentNullException>(() => new WebHookClient(null));
                Assert.Throws<ArgumentNullException>(() => new WebHookClient(null, new HttpClientHandler()));
                Assert.Throws<ArgumentNullException>(() => new WebHookClient(new HttpContenJsonSerializer(), null));
            }

            {
                var client = new WebHookClient(new HttpContenJsonSerializer());

                Assert.NotNull(client);
            }

            {
                var client = new WebHookClient(new HttpContenJsonSerializer(), new HttpClientHandler());

                Assert.NotNull(client);
            }

            {
                var client = new WebHookClient(new HttpContenJsonSerializer(), new HttpClientHandler(), true);

                Assert.NotNull(client);
            }
        }

        [Fact]
        public async Task SendAsync_Tests()
        {
            using (var client = new WebHookClient(new HttpContenJsonSerializer()))
            {
                {
                    Payload payload = null;

                    await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.SendAsync("", "test"));
                    await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.SendAsync("", new Payload { Text = "test" }));
                    await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.SendAsync(WebHookUrl, payload));
                }

                {
                    var message = new StringBuilder("simple message test.")
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

                    var response = await client.SendAsync(WebHookUrl, message);

                    Assert.Equal(ResponseMessage.Ok, response);
                }

                {
                    var message = new StringBuilder("structured message test.")
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

                    var response = await client.SendAsync(WebHookUrl, new Payload { Text = message });

                    Assert.Equal(ResponseMessage.Ok, response);
                }
            }
        }
    }
}
