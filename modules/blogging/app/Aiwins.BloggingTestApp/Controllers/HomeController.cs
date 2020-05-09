using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Blogging;

namespace Aiwins.BloggingTestApp.Controllers
{
    public class HomeController : RocketController
    {
        private readonly BloggingUrlOptions _blogOptions;

        public HomeController(IOptions<BloggingUrlOptions> blogOptions)
        {
            _blogOptions = blogOptions.Value;
        }
        public ActionResult Index()
        {
            var urlPrefix = _blogOptions.RoutePrefix;
            return Redirect(urlPrefix);
        }
    }
}
