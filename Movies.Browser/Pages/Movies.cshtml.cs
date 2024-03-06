using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Browser.Pages
{
    public class MoviesModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public MoviesModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
