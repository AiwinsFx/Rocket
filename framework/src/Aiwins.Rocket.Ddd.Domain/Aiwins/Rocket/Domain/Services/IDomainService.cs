using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Domain.Services {
    /// <summary>
    /// 此接口需要被所有的领域接口实现，以便按照约定注册和标识它们
    /// </summary>
    public interface IDomainService : ITransientDependency {

    }
}