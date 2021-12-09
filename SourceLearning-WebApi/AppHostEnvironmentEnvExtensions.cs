using System;
using Microsoft.Extensions.Hosting;

namespace SourceLearning_WebApi
{
    public static class AppHostEnvironmentEnvExtensions
    {
        public static bool IsTest(this IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment == null)
                throw new ArgumentNullException(nameof(hostEnvironment));
            return hostEnvironment.IsEnvironment(AppEnvironments.Test);
        }
    }

    public static class AppEnvironments
    {
        public static readonly string Test = nameof(Test);
    }
}