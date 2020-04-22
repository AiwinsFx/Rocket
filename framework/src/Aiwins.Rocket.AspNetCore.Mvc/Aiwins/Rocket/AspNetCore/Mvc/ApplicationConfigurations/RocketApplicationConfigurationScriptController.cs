using System.Text;
using System.Threading.Tasks;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Http;
using Aiwins.Rocket.Json;
using Aiwins.Rocket.Minify.Scripts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AspNetCore.Mvc.ApplicationConfigurations {
    [Area ("Rocket")]
    [Route ("Rocket/ApplicationConfigurationScript")]
    [DisableAuditing]
    public class RocketApplicationConfigurationScriptController : RocketController {
        private readonly IRocketApplicationConfigurationAppService _configurationAppService;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly RocketAspNetCoreMvcOptions _options;
        private readonly IJavascriptMinifier _javascriptMinifier;

        public RocketApplicationConfigurationScriptController (
            IRocketApplicationConfigurationAppService configurationAppService,
            IJsonSerializer jsonSerializer,
            IOptions<RocketAspNetCoreMvcOptions> options,
            IJavascriptMinifier javascriptMinifier) {
            _configurationAppService = configurationAppService;
            _jsonSerializer = jsonSerializer;
            _options = options.Value;
            _javascriptMinifier = javascriptMinifier;
        }

        [HttpGet]
        [Produces (MimeTypes.Application.Javascript, MimeTypes.Text.Plain)]
        public async Task<ActionResult> Get () {
            var script = CreateRocketExtendScript (await _configurationAppService.GetAsync ());

            return Content (
                _options.MinifyGeneratedScript == true ?
                _javascriptMinifier.Minify (script) :
                script,
                MimeTypes.Application.Javascript
            );
        }

        private string CreateRocketExtendScript (ApplicationConfigurationDto config) {
            var script = new StringBuilder ();

            script.AppendLine ("(function(){");
            script.AppendLine ();
            script.AppendLine ($"$.extend(true, rocket, {_jsonSerializer.Serialize(config, indented: true)})");
            script.AppendLine ();
            script.AppendLine ("rocket.event.trigger('rocket.configurationInitialized');");
            script.AppendLine ();
            script.Append ("})();");

            return script.ToString ();
        }
    }
}