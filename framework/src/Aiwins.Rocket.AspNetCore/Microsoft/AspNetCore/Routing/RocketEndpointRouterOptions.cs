using System;
using System.Collections.Generic;

namespace Microsoft.AspNetCore.Routing {
    public class RocketEndpointRouterOptions {
        public List<Action<EndpointRouteBuilderContext>> EndpointConfigureActions { get; }

        public RocketEndpointRouterOptions () {
            EndpointConfigureActions = new List<Action<EndpointRouteBuilderContext>> ();
        }
    }
}