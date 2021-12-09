using System;
using Microsoft.AspNetCore.Hosting;

[assembly:HostingStartup(typeof(HostStartupSample.MyCustomStartup))]
namespace HostStartupSample
{
    public class MyCustomStartup:IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            Console.WriteLine("Calling my custom startup");
        }
    }   
}

