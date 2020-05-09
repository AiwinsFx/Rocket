﻿using Aiwins.Rocket.Identity;
using Aiwins.Rocket.ObjectExtending;
using Aiwins.Rocket.Threading;

namespace MyCompanyName.MyProjectName.ObjectExtending
{
    public static class MyProjectNameDomainObjectExtensions
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                /* You can configure extension properties to entities or other object types
                 * defined in the depended modules.
                 * 
                 * If you are using EF Core and want to map the entity extension properties to new
                 * table fields in the database, then configure them in the MyProjectNameEfCoreEntityExtensionMappings
                 *
                 * Example:
                 *
                 * ObjectExtensionManager.Instance
                 *    .AddOrUpdateProperty<IdentityRole, string>("Title");
                 *
                 * See the documentation for more:
                 * https://docs.rocket.io/en/rocket/latest/Object-Extensions
                 */
            });
        }
    }
}