using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SourceLearning_WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);
            var host = hostBuilder.Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuidler = Host.CreateDefaultBuilder(args);
            hostBuidler.ConfigureServices(services => services.AddTransient<IStartupFilter, FirstStartupFilter>());

            hostBuidler.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey,
                    "SkyAPM.Agent.AspNetCore;HostStartupSample");
                webBuilder.UseStartup(typeof(Startup).Assembly.FullName);
            });
            hostBuidler.ConfigureServices(services => services.AddTransient<IStartupFilter, ThirdStartupFilter>());
            return hostBuidler;
        }
    }
}
