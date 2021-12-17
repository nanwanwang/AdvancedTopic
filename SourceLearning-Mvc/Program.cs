using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Console;
using SourceLearning_Mvc.Pages;

namespace SourceLearning_Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging=>
                {
                    logging.SetMinimumLevel(LogLevel.Information);
                    logging.ClearProviders();//.AddConsole();
                    //配置 SimpleConsle
                    // logging.AddSimpleConsole(options =>
                    // {
                    //     options.IncludeScopes = true;
                    //     options.ColorBehavior = LoggerColorBehavior.Default;
                    //     options.SingleLine = true;
                    //     options.TimestampFormat = "yyyy-MM-dd HH:mm:ss";
                    //     options.UseUtcTimestamp = true;
                    // } );
                    
                    //配置 SystemdConsole
                    //logging.AddSystemdConsole();

                    //配置 JsonConsole
                    logging.AddJsonConsole(options =>
                    {
                        options.JsonWriterOptions = new JsonWriterOptions()
                        {
                            Indented = true
                        };
                    });


                    // logging.AddEventLog(eventlogSetting =>
                    // {
                    //     eventlogSetting.LogName = "test";
                    //     eventlogSetting.SourceName = "test";
                    //     eventlogSetting.MachineName = "test";
                    // });

                    // logging.AddFilter("Microsoft", LogLevel.Trace)
                    //     .AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Debug)
                    //     .AddFilter((provider, category, loglevel) =>
                    //     {
                    //         if (provider == typeof(ConsoleLoggerProvider).FullName
                    //             && category == typeof(IndexModel).FullName
                    //             && loglevel <= LogLevel.Warning)
                    //         {
                    //             return false;
                    //         }
                    //
                    //         return true;
                    //     })
                    //     .AddFilter<ConsoleLoggerProvider>((category, loglevel) =>
                    //     {
                    //         if (category == typeof(IndexModel).FullName
                    //             && loglevel <= LogLevel.Warning)
                    //         {
                    //             return false;
                    //         }
                    //
                    //         return true;
                    //     });

                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
