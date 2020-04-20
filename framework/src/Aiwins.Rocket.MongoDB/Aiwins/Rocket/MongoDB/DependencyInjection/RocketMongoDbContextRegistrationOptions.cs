using System;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.MongoDB.DependencyInjection {
    public class RocketMongoDbContextRegistrationOptions : RocketCommonDbContextRegistrationOptions, IRocketMongoDbContextRegistrationOptionsBuilder {
        public RocketMongoDbContextRegistrationOptions (Type originalDbContextType, IServiceCollection services) : base (originalDbContextType, services) { }
    }
}