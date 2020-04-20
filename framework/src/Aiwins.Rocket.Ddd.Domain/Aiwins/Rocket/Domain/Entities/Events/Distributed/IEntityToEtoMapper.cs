using JetBrains.Annotations;

namespace Aiwins.Rocket.Domain.Entities.Events.Distributed
{
    public interface IEntityToEtoMapper
    {
        [CanBeNull]
        object Map(object entityObj);
    }
}
