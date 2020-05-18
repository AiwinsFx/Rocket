using System;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Guids;
using Aiwins.Rocket.Uow;
using Microsoft.AspNetCore.Identity;

namespace Aiwins.Rocket.Identity {
    public class IdentityDataSeeder : ITransientDependency, IIdentityDataSeeder {
        protected IGuidGenerator GuidGenerator { get; }
        protected IIdentityRoleRepository RoleRepository { get; }
        protected IIdentityUserRepository UserRepository { get; }
        protected ILookupNormalizer LookupNormalizer { get; }
        protected IdentityUserManager UserManager { get; }
        protected IdentityRoleManager RoleManager { get; }

        public IdentityDataSeeder (
            IGuidGenerator guidGenerator,
            IIdentityRoleRepository roleRepository,
            IIdentityUserRepository userRepository,
            ILookupNormalizer lookupNormalizer,
            IdentityUserManager userManager,
            IdentityRoleManager roleManager) {
            GuidGenerator = guidGenerator;
            RoleRepository = roleRepository;
            UserRepository = userRepository;
            LookupNormalizer = lookupNormalizer;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        [UnitOfWork]
        public virtual async Task<IdentityDataSeedResult> SeedAsync (
            string adminPhoneNumber,
            string adminPassword,
            Guid? tenantId = null) {
            Check.NotNullOrWhiteSpace (adminPhoneNumber, nameof (adminPhoneNumber));
            Check.NotNullOrWhiteSpace (adminPassword, nameof (adminPassword));

            var result = new IdentityDataSeedResult ();

            //"超管账号" 用户
            const string adminName = "超管账号";
            string adminUserName = adminPhoneNumber;  // 将用户名设置为手机号 userName -> phoneNumber(默认)
            var adminUser = await UserRepository.FindByNormalizedUserNameAsync (
                LookupNormalizer.NormalizeName (adminUserName)
            );

            if (adminUser != null) {
                return result;
            }

            adminUser = new IdentityUser (
                GuidGenerator.Create (),
                adminUserName,
                adminPhoneNumber,
                tenantId
            ) {
                Name = adminName
            };

            (await UserManager.CreateAsync (adminUser, adminPassword)).CheckErrors ();
            result.CreatedAdminUser = true;

            //"超级管理员" 角色
            const string adminRoleName = "超级管理员";
            var adminRole = await RoleRepository.FindByNormalizedNameAsync (LookupNormalizer.NormalizeName (adminRoleName));
            if (adminRole == null) {
                adminRole = new IdentityRole (
                GuidGenerator.Create (),
                adminRoleName,
                tenantId
                ) {
                IsStatic = true,
                IsPublic = true
                };

                (await RoleManager.CreateAsync (adminRole)).CheckErrors ();
                result.CreatedAdminRole = true;
            }

            (await UserManager.AddToRoleAsync (adminUser, adminRoleName)).CheckErrors ();

            return result;
        }
    }
}