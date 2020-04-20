using System;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.BackgroundJobs {
    public class JobExecutionContext : IServiceProviderAccessor {
        public IServiceProvider ServiceProvider { get; }

        public Type JobType { get; }

        public object JobArgs { get; }

        public JobExecutionContext (IServiceProvider serviceProvider, Type jobType, object jobArgs) {
            ServiceProvider = serviceProvider;
            JobType = jobType;
            JobArgs = jobArgs;
        }
    }
}