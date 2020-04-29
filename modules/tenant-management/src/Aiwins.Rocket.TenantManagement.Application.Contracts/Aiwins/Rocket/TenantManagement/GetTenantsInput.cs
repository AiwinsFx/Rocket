using Aiwins.Rocket.Application.Dtos;

namespace Aiwins.Rocket.TenantManagement {
    public class GetTenantsInput : PagedAndSortedResultRequestDto {
        public string Filter { get; set; }
    }
}