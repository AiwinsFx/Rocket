using System;
using Aiwins.ClientSimulation.Clients;

namespace Aiwins.ClientSimulation.Snapshot
{
    [Serializable]
    public class ClientSnapshot
    {
        public ClientState State { get; set; }

        public ScenarioSnapshot Scenario { get; set; }
    }
}