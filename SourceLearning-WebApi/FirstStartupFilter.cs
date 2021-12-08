using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace SourceLearning_WebApi
{
    public class FirstStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) => app =>
        {
            app.Use((context, next) =>
            {
                Console.WriteLine("first");
                return next();
            });
            next(app);
        };
    }

    public class SecondStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) => app =>
        {
            app.Use((context, next) =>
            {
                Console.WriteLine("Second");
                return next();
            });
            next(app);
        };
    }

    public class ThirdStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) => app =>
        {
            app.Use((context, next) =>
            {
                Console.WriteLine("Third");
                return next();
            });
            next(app);
        };
    }
}
