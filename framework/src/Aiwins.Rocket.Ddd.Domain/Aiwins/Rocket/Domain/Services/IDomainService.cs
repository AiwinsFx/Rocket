using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Domain.Services
{
    /// <summary>
    /// This interface must be implemented by all domain services to identify them by convention.
    /// </summary>
    public interface IDomainService : ITransientDependency
    {

    }
}