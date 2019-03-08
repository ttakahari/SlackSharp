using JsonHttpContentConverter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace SlackSharp.DependencyInjection.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddJsonHttpContentConverter_Tests()
        {
            // no-arguments.
            {
                {
                    Assert.Throws<ArgumentNullException>(() =>
                    {
                        var services = new ServiceCollection();

                        services.AddSlackWebHookClient();

                        var service = services.BuildServiceProvider();
                        var client = service.GetService<IWebHookClient>();
                    });

                }

                {
                    var services = new ServiceCollection();

                    services
                        .AddJsonHttpContentConverter<NullJsonHttpContentConverter>()
                        .AddSlackWebHookClient();

                    var service = services.BuildServiceProvider();

                    var client = service.GetService<IWebHookClient>();

                    Assert.NotNull(client);
                    Assert.IsType<WebHookClient>(client);

                    var converter = client
                        .GetType()
                        .GetField("_converter", BindingFlags.NonPublic | BindingFlags.Instance)
                        .GetValue(client);

                    Assert.NotNull(converter);
                    Assert.IsType<NullJsonHttpContentConverter>(converter);
                }
            }

            // arguments.
            {
                var services = new ServiceCollection();

                Assert.Throws<ArgumentNullException>(() => services.AddSlackWebHookClient(null));

                services.AddSlackWebHookClient(new NullJsonHttpContentConverter());

                var service = services.BuildServiceProvider();

                var client = service.GetService<IWebHookClient>();

                Assert.NotNull(client);
                Assert.IsType<WebHookClient>(client);

                var converter = client
                    .GetType()
                    .GetField("_converter", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(client);

                Assert.NotNull(converter);
                Assert.IsType<NullJsonHttpContentConverter>(converter);
            }
        }
    }

    public class NullJsonHttpContentConverter : IJsonHttpContentConverter
    {
        public HttpContent ToJsonHttpContent<T>(T value)
            => throw new NotImplementedException();

        public Task<T> FromJsonHttpContent<T>(HttpContent content)
            => throw new NotImplementedException();
    }
}
