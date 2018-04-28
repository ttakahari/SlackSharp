using SlackSharp.Models;
using System;
using System.Threading.Tasks;

namespace SlackSharp
{
    /// <summary>
    /// A interface that defines methods to send messages with Incoming Webhooks.
    /// </summary>
    public interface IWebHookClient : IDisposable
    {
        /// <summary>
        /// Send a simple message with Incoming WebHooks.
        /// </summary>
        /// <param name="url">Incoming WebHook URL.</param>
        /// <param name="message">The sending message.</param>
        /// <returns>The result to send the message.</returns>
        Task<ResponseMessage> SendAsync(string url, string message);

        /// <summary>
        /// Send a message that includes details with Incoming WebHooks.
        /// </summary>
        /// <param name="url">Incoming WebHook URL.</param>
        /// <param name="payload">The sending message including details.</param>
        /// <returns>The result to send the message.</returns>
        Task<ResponseMessage> SendAsync(string url, Payload payload);
    }
}
