using System.Collections.Generic;

namespace Aiwins.ClientSimulation {
    public class ClientSimulationOptions {
        public List<ScenarioConfiguration> Scenarios { get; }

        public ClientSimulationOptions () {
            Scenarios = new List<ScenarioConfiguration> ();
        }
    }
}