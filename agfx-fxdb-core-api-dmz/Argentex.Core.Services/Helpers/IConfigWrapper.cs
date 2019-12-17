using AutoMapper.Configuration;

namespace Argentex.Core.Service.Helpers
{
    public interface IConfigWrapper
    {
        string Get(string key);
    }
}
