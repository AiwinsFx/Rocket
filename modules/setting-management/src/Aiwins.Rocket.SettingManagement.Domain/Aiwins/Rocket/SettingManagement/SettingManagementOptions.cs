using Aiwins.Rocket.Collections;

namespace Aiwins.Rocket.SettingManagement
{
    public class SettingManagementOptions
    {
        public ITypeList<ISettingManagementProvider> Providers { get; }

        public SettingManagementOptions()
        {
            Providers = new TypeList<ISettingManagementProvider>();
        }
    }
}
