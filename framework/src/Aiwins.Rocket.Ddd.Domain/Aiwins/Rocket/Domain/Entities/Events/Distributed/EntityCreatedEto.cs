using System;
using Aiwins.Rocket.EventBus;

namespace Aiwins.Rocket.Domain.Entities.Events.Distributed
{
    [Serializable]
    [GenericEventName(Postfix = ".Created")]
    public class EntityCreatedEto<TEntityEto>
    {
        public TEntityEto Entity { get; set; }

        public EntityCreatedEto(TEntityEto entity)
        {
            Entity = entity;
        }
    }
}
