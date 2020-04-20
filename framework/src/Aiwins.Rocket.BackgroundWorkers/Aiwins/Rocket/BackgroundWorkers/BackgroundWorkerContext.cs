using System;

namespace Aiwins.Rocket.BackgroundWorkers {
    public class PeriodicBackgroundWorkerContext {
        public IServiceProvider ServiceProvider { get; }

        public PeriodicBackgroundWorkerContext (IServiceProvider serviceProvider) {
            ServiceProvider = serviceProvider;
        }
    }
}