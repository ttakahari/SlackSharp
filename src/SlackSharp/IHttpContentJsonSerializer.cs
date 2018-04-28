using System.Net.Http;
using System.Threading.Tasks;

namespace SlackSharp
{
    /// <summary>
    /// A interface that defines methods to serialize or deserialize between an object and <see cref="HttpContent"/> that includes JSON.
    /// </summary>
    public interface IHttpContentJsonSerializer
    {
        /// <summary>
        /// Serialize a value to <see cref="HttpContent"/> that includes JSON.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The serialized <see cref="HttpContent"/>.</returns>
        HttpContent Serialize<T>(T value);

        /// <summary>
        /// Deserialize <see cref="HttpContent"/> that includes JSON to a value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="content"><see cref="HttpContent"/> that includes JSON.</param>
        /// <returns>The deserialized value.</returns>
        Task<T> Deserialize<T>(HttpContent content);
    }
}
