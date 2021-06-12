using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpToolbox.Rest
{
    public static class Rest
    {
        /// <summary>
        /// Create rest request
        /// </summary>
        /// <typeparam name="T">Request response type</typeparam>
        /// <param name="request">Request</param>
        /// <returns>Rest Response</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.AggregateException"></exception>
        public static RestResponse<T> Request<T>(RestRequest request)
        {
            Task<HttpResponseMessage> responseTask = Request(request);
            Task.WaitAll(responseTask);
            HttpResponseMessage response = responseTask.Result;
            return new RestResponse<T>(response);
        }

        /// <summary>
        /// Create async rest request
        /// </summary>
        /// <typeparam name="T">Request response type</typeparam>
        /// <param name="request">Request</param>
        /// <returns>Rest Response</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.AggregateException"></exception>
        public static async Task<RestResponse<T>> RequestAsync<T>(RestRequest request)
        {
            HttpResponseMessage response = await Request(request);
            return new RestResponse<T>(response);
        }

        /// <summary>
        /// Create async request
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="userAgent">User agent</param>
        /// <returns>Rest Reponse</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        private static async Task<HttpResponseMessage> Request(RestRequest request, string userAgent = "easySharp/1.0")
        {
            if (!request.Headers.ContainsKey("user-agent"))
                request.Headers.Add("user-agent", userAgent);
            HttpClient client = new HttpClient();
            HttpRequestMessage msg = new HttpRequestMessage(request.HttpMethod, request.Url);
            foreach (KeyValuePair<string, string> header in request.Headers)
                msg.Headers.Add(header.Key, header.Value);
            if (request.Body.Any())
                msg.Content = request.Body;
            return await client.SendAsync(msg);
        }
    }
}