using System;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Tracing {
    public class DefaultCorrelationIdProvider : ICorrelationIdProvider, ISingletonDependency {
        public string Get () {
            return CreateNewCorrelationId ();
        }

        protected virtual string CreateNewCorrelationId () {
            return Guid.NewGuid ().ToString ("N");
        }
    }
}