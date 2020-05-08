using System.Threading.Tasks;
using Aiwins.Rocket.Application.Services;
using Aiwins.Rocket.Identity;

namespace Aiwins.Rocket.Account {
    public interface IAccountAppService : IApplicationService {
        Task<IdentityUserDto> RegisterAsync (RegisterDto input);
    }
}