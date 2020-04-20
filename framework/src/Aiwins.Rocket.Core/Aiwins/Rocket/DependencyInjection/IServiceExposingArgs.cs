using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.DependencyInjection {
    public interface IOnServiceExposingContext {
        Type ImplementationType { get; }

        List<Type> ExposedTypes { get; }
    }
}