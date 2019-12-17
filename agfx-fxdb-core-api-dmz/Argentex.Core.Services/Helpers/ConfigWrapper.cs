using Microsoft.Extensions.Configuration;

namespace Argentex.Core.Service.Helpers
{
    public class ConfigWrapper : IConfigWrapper
    {
        private readonly IConfiguration _config;

        public ConfigWrapper(IConfiguration config)
        {
            _config = config;
        }


        public string Get(string key)
        {
            return _config[key];
        }
    }
}
