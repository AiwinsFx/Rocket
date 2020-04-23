using System;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.UI.Navigation
{
    public interface IMenuConfigurationContext : IServiceProviderAccessor
    {
        ApplicationMenu Menu { get; }

        //TODO: Add Localization, Authorization components since they are most used components on menu creation!
    }
}