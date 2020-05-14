using Aiwins.Rocket.Identity;
using Aiwins.Rocket.ObjectExtending;
using Aiwins.Rocket.Threading;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    public static class MyProjectNameEfCoreEntityExtensionMappings
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                /* You can configure entity extension properties for the
                 * entities defined in the used modules.
                 *
                 * The properties defined here becomes table fields.
                 * If you want to use the ExtraProperties dictionary of the entity
                 * instead of creating a new field, then define the property in the
                 * MyProjectNameDomainObjectExtensions class.
                 *
                 * Example:
                 *
                 * ObjectExtensionManager.Instance
                 *    .MapEfCoreProperty<IdentityUser, string>(
                 *        "MyProperty",
                 *        b => b.HasMaxLength(128)
                 *    );
                 *
                 * See the documentation for more:
                 * https://docs.aiwins.cn/en/rocket/latest/Customizing-Application-Modules-Extending-Entities
                 */
            });
        }
    }
}
