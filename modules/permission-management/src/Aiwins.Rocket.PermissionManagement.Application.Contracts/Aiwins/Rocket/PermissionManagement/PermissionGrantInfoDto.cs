using System.Collections.Generic;

namespace Aiwins.Rocket.PermissionManagement {
    public class PermissionGrantInfoDto {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string ParentName { get; set; }

        public bool IsGranted { get; set; }

        public string SelectedScope { get; set; }

        public List<string> AllowedProviders { get; set; }

        public List<PermissionScopeDto> Scopes { get; set; }

        public List<ProviderInfoDto> GrantedProviders { get; set; }
    }
}