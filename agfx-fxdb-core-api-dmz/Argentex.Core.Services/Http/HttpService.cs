using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Argentex.ClientSite.Service.Http
{
    public class HttpService : IHttpService
    {
        private HttpMethod _method = null;
        private string _baseUri = "";
        private string _requestUri = "";
        private HttpContent _content = null;
        private string _bearerToken = "";
        private string _acceptHeader = "";

        protected readonly HttpClient _httpClient;
        protected bool _disposed;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string GenerateUri<T>(string baseUri, T obj)
        {
            var result = new List<string>();

            foreach (var property in typeof(T).GetProperties())
            {
                result.Add(char.ToLower(property.Name[0]) + property.Name.Substring(1) + "=" + property.GetValue(obj));
            }

            return string.Join("?", baseUri, string.Join("&", result));
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = _method,
                RequestUri = new Uri(_requestUri),
            };
            if (_content != null)
                request.Content = _content;

            if (!string.IsNullOrEmpty(_bearerToken))
                request.Headers.Authorization =
                  new AuthenticationHeaderValue("Bearer", _bearerToken);

            if (!string.IsNullOrEmpty(_acceptHeader))
                request.Headers.Accept.Add(
                   new MediaTypeWithQualityHeaderValue(_acceptHeader));

            return await _httpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> HttpGetAsync<T>(string url, T requestModel)
        {
            if (url == null || requestModel == null)
                throw new ArgumentException("url or requestModel parameters cannot be null ");

            return await _httpClient.GetAsync(new Uri(GenerateUri(url, requestModel)));
        }

        public async Task<HttpResponseMessage> HttpGetAsync(string url)
        {
            if (url == null)
                throw new ArgumentException("url or requestModel parameters cannot be null ");

            return await _httpClient.GetAsync(new Uri(url));
        }

        public async Task<HttpResponseMessage> HttpPostAsync<T>(string url, T requestModel)
        {
            if (url == null || requestModel == null)
                throw new ArgumentException("url or requestModel parameters cannot be null ");

            return await _httpClient.PostAsync(url, new JsonContent(requestModel));
        }

        public async Task<T> GetResponseObject<T>(HttpResponseMessage message)
        {
            return JsonConvert.DeserializeObject<T>(
                await message.Content.ReadAsStringAsync());
        }

        public async Task<string> GetResponseAsString(HttpResponseMessage message)
        {
            return await message.Content.ReadAsStringAsync();
        }

        public void AddMethod(HttpMethod method)
        {
            _method = method;
        }

        public void AddRequestUri(string requestUri)
        {
            _requestUri = requestUri;
        }

        public void AddContent(HttpContent content)
        {
            _content = content;
        }

        public void AddBearerToken(string bearerToken)
        {
            _bearerToken = bearerToken;
        }

        public void AddAcceptHeader(string acceptHeader)
        {
            _acceptHeader = acceptHeader;
        }

        public void AddTimeout(TimeSpan timeout)
        {
            _httpClient.Timeout = timeout;
        }


        /// <summary>
        /// disposing = true coming from Dispose()
        /// disposig == false coming from finaliser
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _httpClient?.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
