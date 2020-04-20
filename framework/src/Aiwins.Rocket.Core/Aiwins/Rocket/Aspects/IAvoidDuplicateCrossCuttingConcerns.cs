using System.Collections.Generic;

namespace Aiwins.Rocket.Aspects {
    public interface IAvoidDuplicateCrossCuttingConcerns {
        List<string> AppliedCrossCuttingConcerns { get; }
    }
}