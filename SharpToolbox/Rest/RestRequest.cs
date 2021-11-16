using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharpToolbox.Rest;

public class RestRequest
{
    public RestRequest(HttpMethod method, string url)
    {
        if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri uri))
            throw new ArgumentException("The url passed to the HttpMethod constructor is not a valid HTTP/S url");
        Url = uri;
        HttpMethod = method;
    }

    /// <summary>
    /// Rest URL
    /// </summary>
    public Uri Url { get; }

    /// <summary>
    /// HttpMethod GET/POST etc
    /// </summary>
    public HttpMethod HttpMethod { get; }

    /// <summary>
    /// Rest body
    /// </summary>
    public MultipartFormDataContent Body { get; private set; }

    /// <summary>
    /// Rest headers
    /// </summary>
    public Dictionary<string, string> Headers { get; } = new();

    /// <summary>
    /// Has fields
    /// </summary>
    public bool HasFields { get; private set; }

    /// <summary>
    /// As explicit body
    /// </summary>
    public bool HasExplicitBody { get; private set; }

    /// <summary>
    /// Verify if we can add a new body
    /// </summary>
    public bool CanAddBody => (HttpMethod != HttpMethod.Get) && !HasFields;

    /// <summary>
    /// Verify if we can add a new field
    /// </summary>
    public bool CanAddField => (HttpMethod != HttpMethod.Get) && !HasExplicitBody;

    /// <summary>
    /// Add header 
    /// </summary>
    /// <param name="name">Header Name</param>
    /// <param name="value">Header Value</param>
    /// <returns>This</returns>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.ArgumentNullException"></exception>
    public RestRequest AddHeader(string name, string value)
    {
        Headers.Add(name, value);
        return this;
    }

    /// <summary>
    /// Add headers
    /// </summary>
    /// <param name="headers">Headers</param>
    /// <returns>This</returns>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.ArgumentNullException"></exception>
    public RestRequest AddHeaders(Dictionary<string, string> headers)
    {
        foreach (KeyValuePair<string, string> kv in headers)
            Headers.Add(kv.Key, kv.Value);
        return this;
    }

    /// <summary>
    /// Add a field
    /// </summary>
    /// <param name="name">Field Name</param>
    /// <param name="value">Field Value</param>
    /// <returns>This</returns>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.ArgumentNullException"></exception>
    public RestRequest AddField(string name, string value)
    {
        if (CanAddField)
        {
            Body.Add(new StringContent(value), name);
            HasFields = true;
        }

        return this;
    }

    /// <summary>
    /// Add a field
    /// </summary>
    /// <param name="name">Field name</param>
    /// <param name="imageData">Field data ( image )</param>
    /// <returns>This</returns>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.FormatException"></exception>
    public RestRequest AddField(string name, byte[] imageData)
    {
        if (CanAddField)
        {
            ByteArrayContent content = new ByteArrayContent(imageData);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("image.jpeg");
            Body.Add(content, name, "image.jpg");
            HasFields = true;
        }

        return this;
    }

    /// <summary>
    /// Add a field
    /// </summary>
    /// <param name="value">Stream value</param>
    /// <returns>This</returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    public RestRequest AddField(Stream value)
    {
        if (CanAddField)
        {
            Body.Add(new StreamContent(value));
            HasFields = true;
        }

        return this;
    }

    /// <summary>
    /// Add fields
    /// </summary>
    /// <param name="parameters">params</param>
    /// <returns>This</returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    public RestRequest AddFields(Dictionary<string, object> parameters)
    {
        if (CanAddField)
        {
            Body.Add(new FormUrlEncodedContent(parameters.Where(kv => kv.Value is string)
                                                         .Select(kv => new KeyValuePair<string, string>(kv.Key, kv.Value as string))));
            foreach (var stream in parameters.Where(kv => kv.Value is Stream).Select(kv => kv.Value))
                Body.Add(new StreamContent(stream as Stream));
            HasFields = true;
        }

        return this;
    }

    /// <summary>
    /// Add body
    /// </summary>
    /// <param name="body">body</param>
    /// <returns>This</returns>
    public RestRequest AddBody(string body)
    {
        if (CanAddBody)
        {
            Body = new MultipartFormDataContent {new StringContent(body)};
            HasExplicitBody = true;
        }

        return this;
    }


    /// <summary>
    /// Add body
    /// </summary>
    /// <typeparam name="T">Body Type</typeparam>
    /// <param name="body">Body</param>
    /// <returns>This</returns>
    public RestRequest AddBody<T>(T body)
    {
        if (CanAddBody)
        {
            Body = new MultipartFormDataContent {new StringContent(JsonSerializer.Serialize(body))};
            HasExplicitBody = true;
        }

        return this;
    }

    /// <summary>
    /// Return the response as a string
    /// </summary>
    /// <returns>String response</returns>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.ObjectDisposedException"></exception>
    /// <exception cref="System.AggregateException"></exception>
    public RestResponse<string> AsString()
    {
        return Rest.Request<string>(this);
    }

    /// <summary>
    /// Return the response as a string asynchronously
    /// </summary>
    /// <returns>String response</returns>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.ObjectDisposedException"></exception>
    /// <exception cref="System.AggregateException"></exception>
    public Task<RestResponse<string>> AsStringAsync()
    {
        return Rest.RequestAsync<string>(this);
    }

    /// <summary>
    /// Return the response as binary
    /// </summary>
    /// <returns>Binary response</returns>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.ObjectDisposedException"></exception>
    /// <exception cref="System.AggregateException"></exception>
    public RestResponse<Stream> AsBinary()
    {
        return Rest.Request<Stream>(this);
    }

    /// <summary>
    /// Return the response as binary asynchronously
    /// </summary>
    /// <returns>Binary response</returns>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.ObjectDisposedException"></exception>
    /// <exception cref="System.AggregateException"></exception>
    public Task<RestResponse<Stream>> AsBinaryAsync()
    {
        return Rest.RequestAsync<Stream>(this);
    }

    /// <summary>
    /// Return the response as json
    /// </summary>
    /// <typeparam name="T">Class to be cast</typeparam>
    /// <returns>Json response</returns>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.ObjectDisposedException"></exception>
    /// <exception cref="System.AggregateException"></exception>
    public RestResponse<T> AsJson<T>()
    {
        return Rest.Request<T>(this);
    }

    /// <summary>
    /// Return the response as json asynchronously
    /// </summary>
    /// <typeparam name="T">Class to be cast</typeparam>
    /// <returns>Json response</returns>
    /// <exception cref="System.ArgumentException"></exception>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.ObjectDisposedException"></exception>
    /// <exception cref="System.AggregateException"></exception>
    public Task<RestResponse<T>> AsJsonAsync<T>()
    {
        return Rest.RequestAsync<T>(this);
    }
}