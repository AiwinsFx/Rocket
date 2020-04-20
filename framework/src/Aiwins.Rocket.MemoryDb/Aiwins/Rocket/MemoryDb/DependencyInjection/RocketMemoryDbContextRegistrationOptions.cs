using System;
using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.MemoryDb.DependencyInjection {
    public class RocketMemoryDbContextRegistrationOptions : RocketCommonDbContextRegistrationOptions, IRocketMemoryDbContextRegistrationOptionsBuilder {
        public RocketMemoryDbContextRegistrationOptions (Type originalDbContextType, IServiceCollection services) : base (originalDbContextType, services) { }
    }
}