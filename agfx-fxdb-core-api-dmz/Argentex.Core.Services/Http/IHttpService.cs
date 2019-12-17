using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Argentex.ClientSite.Service.Http
{
    public interface IHttpService : IDisposable
    {
        string GenerateUri<T>(string baseUri, T obj);
        Task<T> GetResponseObject<T>(HttpResponseMessage message);
        Task<string> GetResponseAsString(HttpResponseMessage message);
        Task<HttpResponseMessage> SendAsync();
        void AddMethod(HttpMethod method);
        void AddRequestUri(string requestUri);
        void AddContent(HttpContent content);
        void AddBearerToken(string bearerToken);
        void AddAcceptHeader(string acceptHeader);
        void AddTimeout(TimeSpan timeout);

    }
}