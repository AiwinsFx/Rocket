using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Services;

namespace Aiwins.Rocket.Identity
{
    //TODO: Rename to IdentityClaimTypeManager in v2.0!
    public class IdenityClaimTypeManager : DomainService
    {
        protected IIdentityClaimTypeRepository IdentityClaimTypeRepository { get; }

        public IdenityClaimTypeManager(IIdentityClaimTypeRepository identityClaimTypeRepository)
        {
            IdentityClaimTypeRepository = identityClaimTypeRepository;
        }

        public virtual async Task<IdentityClaimType> CreateAsync(IdentityClaimType claimType)
        {
            if (await IdentityClaimTypeRepository.AnyAsync(claimType.Name))
            {
                throw new RocketException($"Name Exist: {claimType.Name}");
            }

            return await IdentityClaimTypeRepository.InsertAsync(claimType);
        }

        public virtual async Task<IdentityClaimType> UpdateAsync(IdentityClaimType claimType)
        {
            if (await IdentityClaimTypeRepository.AnyAsync(claimType.Name, claimType.Id))
            {
                throw new RocketException($"Name Exist: {claimType.Name}");
            }

            if (claimType.IsStatic)
            {
                throw new RocketException($"Can not update a static ClaimType.");
            }
            

            return await IdentityClaimTypeRepository.UpdateAsync(claimType);
        }
    }
}
