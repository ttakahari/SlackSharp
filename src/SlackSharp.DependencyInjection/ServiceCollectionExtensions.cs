using JsonHttpContentConverter;
using SlackSharp;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions of <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// <para>Add <see cref="IWebHookClient"/> to resolve an instance with DI.</para>
        /// <para>If you use this method, you have to add an instance of <see cref="IJsonHttpContentConverter"/> to <see cref="IServiceCollection"/>.</para>
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>Added <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddSlackWebHookClient(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddTransient<IWebHookClient>(service => new WebHookClient(service.GetService<IJsonHttpContentConverter>()));

            return services;
        }

        /// <summary>
        /// Add <see cref="IWebHookClient"/> with an instance of <see cref="IJsonHttpContentConverter"/> to resolve an instance with DI.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="converter">An instance of <see cref="IJsonHttpContentConverter"/>.</param>
        /// <returns>Added <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddSlackWebHookClient(this IServiceCollection services, IJsonHttpContentConverter converter)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (converter == null) throw new ArgumentNullException(nameof(converter));

            services.AddSingleton(converter);
            services.AddTransient<IWebHookClient>(service => new WebHookClient(service.GetService<IJsonHttpContentConverter>()));

            return services;
        }
    }
}
