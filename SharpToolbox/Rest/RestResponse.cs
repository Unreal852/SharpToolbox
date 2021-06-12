using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharpToolbox.Rest
{
    public class RestResponse<T>
    {
        public RestResponse(HttpResponseMessage response)
        {
            Code = (int) response.StatusCode;
            Task<Stream> streamTask = response.Content.ReadAsStreamAsync();
            Task.WaitAll(streamTask);
            Raw = streamTask.Result;

            if (typeof(T) == typeof(string))
            {
                Task<string> stringTask = response.Content.ReadAsStringAsync();
                Task.WaitAll(stringTask);
                Body = (T) (object) stringTask.Result;
            }
            else if (typeof(Stream).IsAssignableFrom(typeof(T)))
                Body = (T) (object) Raw;
            else
            {
                Task<string> stringTask = response.Content.ReadAsStringAsync();
                Task.WaitAll(stringTask);
                Body = JsonSerializer.Deserialize<T>(stringTask.Result);
            }

            foreach (KeyValuePair<string, IEnumerable<string>> header in response.Headers)
                Headers.Add(header.Key, header.Value.First());
        }

        /// <summary>
        /// Response status code
        /// </summary>
        public int Code { get; private set; }

        /// <summary>
        /// Reponse headers
        /// </summary>
        public Dictionary<String, String> Headers { get; private set; } = new Dictionary<string, string>();

        /// <summary>
        /// Reponse body
        /// </summary>
        public T Body { get; set; }

        /// <summary>
        /// Response raw data
        /// </summary>
        public Stream Raw { get; private set; }
    }
}