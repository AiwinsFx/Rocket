using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Http;
using Aiwins.Rocket.Http.ProxyScripting;
using Aiwins.Rocket.Minify.Scripts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AspNetCore.Mvc.ProxyScripting {
    [Area ("Rocket")]
    [Route ("Rocket/ServiceProxyScript")]
    [DisableAuditing]
    public class RocketServiceProxyScriptController : RocketController {
        private readonly IProxyScriptManager _proxyScriptManager;
        private readonly RocketAspNetCoreMvcOptions _options;
        private readonly IJavascriptMinifier _javascriptMinifier;

        public RocketServiceProxyScriptController (IProxyScriptManager proxyScriptManager,
            IOptions<RocketAspNetCoreMvcOptions> options,
            IJavascriptMinifier javascriptMinifier) {
            _proxyScriptManager = proxyScriptManager;
            _options = options.Value;
            _javascriptMinifier = javascriptMinifier;
        }

        [HttpGet]
        [Produces (MimeTypes.Application.Javascript, MimeTypes.Text.Plain)]
        public ActionResult GetAll (ServiceProxyGenerationModel model) {
            model.Normalize ();

            var script = _proxyScriptManager.GetScript (model.CreateOptions ());

            return Content (
                _options.MinifyGeneratedScript == true ?
                _javascriptMinifier.Minify (script) :
                script,
                MimeTypes.Application.Javascript
            );
        }
    }
}