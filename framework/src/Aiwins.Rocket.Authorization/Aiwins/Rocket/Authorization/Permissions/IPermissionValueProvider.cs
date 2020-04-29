using System.Threading.Tasks;

namespace Aiwins.Rocket.Authorization.Permissions {
    public interface IPermissionValueProvider {
        string Name { get; }
        Task<PermissionGrantResult> GetResultAsync (PermissionValueCheckContext context);
    }
}