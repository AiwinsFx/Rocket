using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Aiwins.Rocket.Authorization {
    public interface IRocketAuthorizationPolicyProvider : IAuthorizationPolicyProvider {
        Task<List<string>> GetPoliciesNamesAsync ();
    }
}