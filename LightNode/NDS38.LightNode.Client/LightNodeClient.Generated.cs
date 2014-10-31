using LightNode.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace LightNode.Client
{
    public partial class LightNodeClient : _IGreeting
    {
        static IContentFormatter defaultContentFormatter = new LightNode.Formatter.JsonNetContentFormatter();
        readonly string rootEndPoint;
        readonly HttpClient httpClient;

        public long MaxResponseContentBufferSize
        {
            get { return httpClient.MaxResponseContentBufferSize; }
            set { httpClient.MaxResponseContentBufferSize = value; }
        }

        public TimeSpan Timeout
        {
            get { return httpClient.Timeout; }
            set { httpClient.Timeout = value; }
        }

        IContentFormatter contentFormatter;
        public IContentFormatter ContentFormatter
        {
            get { return contentFormatter = (contentFormatter ?? defaultContentFormatter); }
            set { contentFormatter = value; }
        }

        public _IGreeting Greeting { get { return this; } }

        public LightNodeClient(string rootEndPoint)
        {
            this.httpClient = new HttpClient();
            this.rootEndPoint = rootEndPoint.TrimEnd('/');
            this.ContentFormatter = defaultContentFormatter;
        }

        public LightNodeClient(string rootEndPoint, HttpMessageHandler innerHandler)
        {
            this.httpClient = new HttpClient(innerHandler);
            this.rootEndPoint = rootEndPoint.TrimEnd('/');
            this.ContentFormatter = defaultContentFormatter;
        }

        public LightNodeClient(string rootEndPoint, HttpMessageHandler innerHandler, bool disposeHandler)
        {
            this.httpClient = new HttpClient(innerHandler, disposeHandler);
            this.rootEndPoint = rootEndPoint.TrimEnd('/');
            this.ContentFormatter = defaultContentFormatter;
        }

        protected virtual async Task PostAsync(string method, FormUrlEncodedContent content, CancellationToken cancellationToken)
        {
            var response = await httpClient.PostAsync(rootEndPoint + method, content, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        protected virtual async Task<T> PostAsync<T>(string method, FormUrlEncodedContent content, CancellationToken cancellationToken)
        {
            if (ContentFormatter.MediaType != null) content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(ContentFormatter.MediaType);
            var response = await httpClient.PostAsync(rootEndPoint + method, content, cancellationToken).ConfigureAwait(false);
            using (var stream = await response.EnsureSuccessStatusCode().Content.ReadAsStreamAsync().ConfigureAwait(false))
            {
                return (T)ContentFormatter.Deserialize(typeof(T), stream);
            }
        }

        #region _IGreeting

        System.Threading.Tasks.Task<System.String> _IGreeting.GreetAsync(System.String name, System.Threading.CancellationToken cancellationToken)
        {
            var list = new List<KeyValuePair<string, string>>(1);
            if (name != null) list.Add(new KeyValuePair<string, string>("name", name));

            return PostAsync<System.String>("/Greeting/Greet", new FormUrlEncodedContent(list), cancellationToken);
        }

        #endregion

    }

    public interface _IGreeting
    {
        System.Threading.Tasks.Task<System.String> GreetAsync(System.String name, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    }

}

