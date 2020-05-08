using Aiwins.Rocket.Application.Dtos;

namespace Aiwins.Rocket.Identity {
    public class GetIdentityUsersInput : PagedAndSortedResultRequestDto {
        public string Filter { get; set; }
    }
}