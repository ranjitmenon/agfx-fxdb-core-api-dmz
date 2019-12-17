using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Argentex.Core.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel(c => c.AddServerHeader = false)
                //todo remove from live app
                .UseSetting("detailedErrors", "true")
                //.UseIISIntegration()
                .UseStartup<Startup>()
                //todo remove from live app
                .CaptureStartupErrors(true)
                .Build();
    }
}
