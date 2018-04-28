using System.Net.Http;
using System.Threading.Tasks;

namespace SlackSharp
{
    public interface IHttpContentJsonSerializer
    {
        HttpContent Serialize<T>(T value);

        Task<T> Deserialize<T>(HttpContent content);
    }
}
