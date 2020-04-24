using System;
using Aiwins.ClientSimulation.Scenarios;
using Aiwins.ClientSimulation.Snapshot;

namespace Aiwins.ClientSimulation.Clients
{
    public interface IClient
    {
        event EventHandler Stopped;

        ClientState State { get; }

        void Initialize(Scenario scenario);

        void Start();

        void Stop();

        ClientSnapshot CreateSnapshot();
    }
}