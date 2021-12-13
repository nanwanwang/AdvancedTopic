using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

namespace SourceLearning_WebApi
{
    public class Book2Validation:IValidateOptions<Book2Options>
    {
        public ValidateOptionsResult Validate(string name, Book2Options options)
        {
            var failtures = new List<string>();
            if (!(options.Id >= 1 && options.Id <= 1000))
            {
                failtures.Add($"必须 1 <= {nameof(options.Id)} <= 1000");
            }

            if (!(options.Name.Length >= 1 && options.Name.Length <= 10))
            {
                failtures.Add($"必须 1 <= {nameof(options.Name)} <= 10");
            }

            if (failtures.Any())
            {
                return ValidateOptionsResult.Fail(failtures);
            }

            return ValidateOptionsResult.Success;
        }
    }
}