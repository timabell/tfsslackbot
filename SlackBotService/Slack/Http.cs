using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SlackBot.Slack
{
    /// <summary>
    /// Helpers to make HTTP requests.
    /// </summary>
    class Http
    {
        /// <summary>
        /// Creates a new <see cref="HttpWebRequest"/> with the specified URL and GET arguments.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The <see cref="HttpWebRequest"/>.</returns>
        public static HttpWebRequest Get(string url, params string[] args)
        {
            var sb = new StringBuilder(url);
            for (var i = 0; i < args.Length; i += 2)
            {
                sb.Append(i == 0 ? '?' : '&')
                    .Append(Uri.EscapeDataString(args[i]))
                    .Append('=')
                    .Append(Uri.EscapeDataString(args[i + 1]));
            }

            var result = HttpWebRequest.CreateHttp(sb.ToString());
            result.Method = "GET";
            return result;
        }

        /// <summary>
        /// Asynchronously gets a value and deserializes it to JSON
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="System.Threading.CancellationToken.None"/>.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>
        /// A <see cref="Task{JsonObject}"/> that represents the asynchronous gets operation.
        /// </returns>
        public static async Task<JsonObject> GetJsonAsync(string url, CancellationToken cancellationToken, params string[] args)
        {
            var request = Get(url, args);
            request.Accept = "text/json";

            using (var response = await request.GetResponseAsync())
            using (var stream = response.GetResponseStream())
            using (var textReader = new StreamReader(stream))
            {
                return JsonObject.Parse(await textReader.ReadToEndAsync());
            }
        }

        /// <summary>
        /// Asynchronously gets a value and deserializes it to JSON
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="json">The JSON body.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="System.Threading.CancellationToken.None"/>.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>
        /// A <see cref="Task{JsonObject}"/> that represents the asynchronous gets operation.
        /// </returns>
        public static async Task<JsonObject> PostJsonAsync(string url, IEnumerable<KeyValuePair<string, object>> json, CancellationToken cancellationToken, params string[] args)
        {
            var request = Get(url, args);
            request.Accept = "text/json";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            
            using (var requestStream = await request.GetRequestStreamAsync())
            using (var sr = new StreamWriter(requestStream))
            {
                var first = true;
                foreach (var item in json)
                {
                    if (!first) sr.Write('&');
                    first = false;

                    sr.Write(Uri.EscapeDataString(item.Key));
                    sr.Write('=');
                    sr.Write(Uri.EscapeDataString(item.Value as string ?? JsonSerializer.SerializeToString(item.Value)));
                }
            }

            using (var response = await request.GetResponseAsync())
            using (var stream = response.GetResponseStream())
            using (var textReader = new StreamReader(stream))
            {
                return JsonObject.Parse(await textReader.ReadToEndAsync());
            }
        }
    }
}
