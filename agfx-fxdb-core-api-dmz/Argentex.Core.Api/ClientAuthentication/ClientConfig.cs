using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Argentex.Core.Identity.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Argentex.Core.Api.ClientAuthentication
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [DebuggerDisplay("{ClientId} - {DisplayName}")]
    public class ClientConfig
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string DisplayName { get; set; }
        public List<string> Permissions { get; set; }
    }
}
