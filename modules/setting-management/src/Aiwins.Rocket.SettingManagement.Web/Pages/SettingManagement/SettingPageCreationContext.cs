using System;
using System.Collections.Generic;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.SettingManagement.Web.Pages.SettingManagement
{
    public class SettingPageCreationContext : IServiceProviderAccessor
    {
        public IServiceProvider ServiceProvider { get; }

        public List<SettingPageGroup> Groups { get; }

        public SettingPageCreationContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;

            Groups = new List<SettingPageGroup>();
        }
    }
}