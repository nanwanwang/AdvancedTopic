using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SourceLearning_WebApi
{
    public static class EntityFrameworkExtensions
    {
        public static IConfigurationBuilder AddEFConfiguration(this IConfigurationBuilder builder,
            Action<DbContextOptionsBuilder> optionAction)
        {
            return builder.Add(new EFConfigurationSource(optionAction));
        }
    }
}