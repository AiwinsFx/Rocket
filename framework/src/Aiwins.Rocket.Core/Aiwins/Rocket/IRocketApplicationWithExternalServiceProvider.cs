using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket {
    public interface IRocketApplicationWithExternalServiceProvider : IRocketApplication {
        void Initialize ([NotNull] IServiceProvider serviceProvider);
    }
}