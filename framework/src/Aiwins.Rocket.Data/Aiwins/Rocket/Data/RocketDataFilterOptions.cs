using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.Data {
    public class RocketDataFilterOptions {
        public Dictionary<Type, DataFilterState> DefaultStates { get; }

        public RocketDataFilterOptions () {
            DefaultStates = new Dictionary<Type, DataFilterState> ();
        }
    }
}