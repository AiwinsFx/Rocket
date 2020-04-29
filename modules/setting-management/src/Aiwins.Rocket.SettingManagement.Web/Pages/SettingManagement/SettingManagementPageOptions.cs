using System.Collections.Generic;

namespace Aiwins.Rocket.SettingManagement.Web.Pages.SettingManagement {
    public class SettingManagementPageOptions {
        public List<ISettingPageContributor> Contributors { get; }

        public SettingManagementPageOptions () {
            Contributors = new List<ISettingPageContributor> ();
        }
    }
}