using System;
using System.Threading;
using Aiwins.ClientSimulation.Scenarios;
using Aiwins.ClientSimulation.Snapshot;
using Aiwins.Rocket;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Threading;

namespace Aiwins.ClientSimulation.Clients {
    public class Client : IClient, ITransientDependency {
        public event EventHandler Stopped;

        public ClientState State {
            get => _state;
            private set => _state = value;
        }
        private volatile ClientState _state;

        protected Scenario Scenario { get; private set; }
        protected object SyncLock { get; } = new object ();

        protected Thread ClientThread;

        public void Initialize (Scenario scenario) {
            lock (SyncLock) {
                if (State != ClientState.Stopped) {
                    throw new UserFriendlyException ($"Client should be stopped to be able to initialize it. Current state is '{State}'.");
                }

                Scenario = scenario;
            }
        }

        public void Start () {
            lock (SyncLock) {
                if (State != ClientState.Stopped) {
                    throw new UserFriendlyException ($"Client should be stopped to be able to start it. Current state is '{State}'.");
                }

                State = ClientState.Running;

                Scenario.Reset ();
                ClientThread = new Thread (Run);
                ClientThread.Start ();
            }
        }

        public void Stop () {
            lock (SyncLock) {
                if (State != ClientState.Running) {
                    return;
                }

                State = ClientState.Stopping;
            }
        }

        public ClientSnapshot CreateSnapshot () {
            lock (SyncLock) {
                return new ClientSnapshot {
                    State = State,
                        Scenario = Scenario.CreateSnapshot ()
                };
            }
        }

        private void Run () {
            while (true) {
                lock (SyncLock) {
                    if (State != ClientState.Running) {
                        State = ClientState.Stopped;
                        ClientThread = null;
                        Stopped.InvokeSafely (this);
                        break;
                    }
                }

                AsyncHelper.RunSync (() => Scenario.ProceedAsync ());
            }
        }
    }
}