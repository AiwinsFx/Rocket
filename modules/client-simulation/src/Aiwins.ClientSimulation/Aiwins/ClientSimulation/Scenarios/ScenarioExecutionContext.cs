using System;
using System.Collections.Generic;

namespace Aiwins.ClientSimulation.Scenarios {
    public class ScenarioExecutionContext {
        public IServiceProvider ServiceProvider { get; }

        public Dictionary<string, object> Properties { get; }

        public ScenarioExecutionContext (IServiceProvider serviceProvider) {
            ServiceProvider = serviceProvider;
            Properties = new Dictionary<string, object> ();
        }

        public virtual void Reset () {
            Properties.Clear ();
        }
    }
}