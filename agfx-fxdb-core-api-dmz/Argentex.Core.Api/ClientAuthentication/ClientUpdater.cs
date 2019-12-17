using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;

namespace Argentex.Core.Api.ClientAuthentication
{
    internal class ClientUpdater: IDisposable
    {
        private readonly OpenIddictApplicationManager<OpenIddictApplication> _appManager;
        private readonly IMapper _mapper;
        private readonly IServiceScope _scope;
        internal ClientUpdater(IServiceProvider services)
        {
            _scope = services.GetRequiredService<IServiceProvider>().CreateScope();
            _appManager = _scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication>>();
            _mapper = _scope.ServiceProvider.GetRequiredService<IMapper>();
        }

        public void SynchroniseClients(IEnumerable<ClientConfig> configs)
        {
            var tasks = configs.Select(SetClient).ToArray(); //TODO: Update clients, and remove clients from environment that do not exist in the config list
            Task.WaitAll(tasks);
        }

        private async Task SetClient(ClientConfig config)
        {
            var existingClient = await GetExistingClient(config.ClientId);
            if (existingClient == null)
            {
                var descriptor = _mapper.Map<OpenIddictApplicationDescriptor>(config);
                config.Permissions.ForEach(p=> descriptor.Permissions.Add(p));
                await _appManager.CreateAsync(descriptor);
            }
        }

        private async Task<OpenIddictApplication> GetExistingClient(string clientId) => await _appManager.FindByClientIdAsync(clientId);

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}