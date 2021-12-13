using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Ini;
using Microsoft.Extensions.Configuration.Json;

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
            
            hostBuidler.ConfigureServices(services =>
            {
                services.AddTransient<IStartupFilter, FirstStartupFilter>();
               
            });
            hostBuidler.ConfigureAppConfiguration((context, config) =>
            {
                //config.Sources.Clear();
                var env = context.HostingEnvironment;
                // config.AddJsonFile("appsettings.json", true, true)
                //     .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
                //IConfigurationBuilder
                //IniConfigurationProvider
                 //JsonConfigurationSource
                //ConfigurationRoot
                
                // config.AddEnvironmentVariables("MY_");
                 // config.AddXmlFile("appsettings.xml", true, true);
                // config.AddIniFile("appsettings.ini", true, true);
                // config.AddInMemoryCollection(new Dictionary<string, string>
                // {
                //     ["Book:Name"] = "Memmory book name",
                //     ["Book:Authors:0"] = "Memory book author A",
                //     ["Book:Authors:1"] = "Memory book author B",
                //     ["Book:Bookmark:Remarks"] = "Memory bookmark remarks"
                // });
               // config.AddEFConfiguration(options => options.UseInMemoryDatabase("configdb"));
                // config.AddCommandLine(args);
            });

            hostBuidler.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey,
                    "SkyAPM.Agent.AspNetCore;HostStartupSample");
                webBuilder.UseStartup(typeof(Startup).Assembly.FullName);
              
                //webBuilder.UseUrls("http://localhost:5002","https://localhost:5003");
            });
            hostBuidler.ConfigureServices(services =>
            {
                services.AddTransient<IStartupFilter, ThirdStartupFilter>();
                services.Configure<BookOptions>(book =>
                {
                    book.Name = "delegate book name";
                    book.Authors = new List<string>() { "aaa", "aaa" };
                    book.Bookmark = new BookmarkOptions() { Remarks = "delegate bookremark remarks" };

                });
            });
            hostBuidler.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            return hostBuidler;
        }
    }
}
