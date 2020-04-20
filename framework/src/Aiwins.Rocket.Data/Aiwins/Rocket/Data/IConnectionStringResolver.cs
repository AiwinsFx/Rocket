using JetBrains.Annotations;

namespace Aiwins.Rocket.Data {
    public interface IConnectionStringResolver {
        [NotNull]
        string Resolve (string connectionStringName = null);
    }
}