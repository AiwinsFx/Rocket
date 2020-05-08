using System;
using System.Threading.Tasks;

namespace Aiwins.Rocket.Identity {
    public interface IUserRoleFinder {
        Task<string[]> GetRolesAsync (Guid userId);
    }
}