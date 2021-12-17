using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SourceLearning_Mvc.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<IndexModel>();
        }

        public void OnGet()
        {
            _logger.LogWarning("onget()");

        }
    }
}
