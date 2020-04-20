using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.Http.ProxyScripting.Configuration {
    public class RocketApiProxyScriptingOptions {
        public IDictionary<string, Type> Generators { get; }

        public RocketApiProxyScriptingOptions () {
            Generators = new Dictionary<string, Type> ();
        }
    }
}