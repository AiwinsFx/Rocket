using System.Collections.Generic;

namespace Aiwins.Rocket.PermissionManagement {
    public class GetPermissionListResultDto {
        public string EntityDisplayName { get; set; }

        public List<PermissionGroupDto> Groups { get; set; }
    }
}