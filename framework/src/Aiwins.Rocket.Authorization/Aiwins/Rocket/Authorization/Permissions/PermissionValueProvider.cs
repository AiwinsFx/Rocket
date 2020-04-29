using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Authorization.Permissions {
    public abstract class PermissionValueProvider : IPermissionValueProvider, ITransientDependency {
        public abstract string Name { get; }

        protected IPermissionStore PermissionStore { get; }

        protected PermissionValueProvider (IPermissionStore permissionStore) {
            PermissionStore = permissionStore;
        }

        public abstract Task<PermissionGrantResult> GetResultAsync (PermissionValueCheckContext context);
    }
}