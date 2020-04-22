using System;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.Conventions {
    [DisableConventionalRegistration]
    public class RocketServiceConventionWrapper : IApplicationModelConvention {
        private readonly Lazy<IRocketServiceConvention> _convention;

        public RocketServiceConventionWrapper (IServiceCollection services) {
            _convention = services.GetRequiredServiceLazy<IRocketServiceConvention> ();
        }

        public void Apply (ApplicationModel application) {
            _convention.Value.Apply (application);
        }
    }
}