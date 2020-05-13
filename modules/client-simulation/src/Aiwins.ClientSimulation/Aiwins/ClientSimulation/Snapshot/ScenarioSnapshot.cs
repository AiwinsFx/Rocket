using System;
using System.Collections.Generic;

namespace Aiwins.ClientSimulation.Snapshot {
    [Serializable]
    public class ScenarioSnapshot {
        public string DisplayText { get; set; }

        public List<ScenarioStepSnapshot> Steps { get; set; }

        public ScenarioStepSnapshot CurrentStep { get; set; }
    }
}