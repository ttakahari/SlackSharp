using SlackSharp.Models;
using System;
using System.Threading.Tasks;

namespace SlackSharp
{
    public interface IWebHookClient : IDisposable
    {
        Task<ResponseMessage> SendAsync(string url, string message);

        Task<ResponseMessage> SendAsync(string url, Payload payload);
    }
}
