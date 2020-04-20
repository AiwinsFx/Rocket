using System;
using System.Collections.Generic;
using Aiwins.Rocket.Aspects;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Http.Modeling {
    public class RocketApiDescriptionModelOptions {
        public HashSet<Type> IgnoredInterfaces { get; }

        public RocketApiDescriptionModelOptions () {
            IgnoredInterfaces = new HashSet<Type> {
                typeof (ITransientDependency),
                typeof (ISingletonDependency),
                typeof (IDisposable),
                typeof (IAvoidDuplicateCrossCuttingConcerns)
            };
        }
    }
}