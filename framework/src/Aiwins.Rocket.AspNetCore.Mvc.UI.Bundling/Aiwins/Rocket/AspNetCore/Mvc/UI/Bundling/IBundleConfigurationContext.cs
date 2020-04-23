using System.Collections.Generic;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling
{
    public interface IBundleConfigurationContext : IServiceProviderAccessor
    {
        List<string> Files { get; }
    }
}