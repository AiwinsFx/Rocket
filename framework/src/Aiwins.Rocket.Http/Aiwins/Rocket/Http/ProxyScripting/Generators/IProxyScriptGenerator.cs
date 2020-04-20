using Aiwins.Rocket.Http.Modeling;

namespace Aiwins.Rocket.Http.ProxyScripting.Generators {
    public interface IProxyScriptGenerator {
        string CreateScript (ApplicationApiDescriptionModel model);
    }
}