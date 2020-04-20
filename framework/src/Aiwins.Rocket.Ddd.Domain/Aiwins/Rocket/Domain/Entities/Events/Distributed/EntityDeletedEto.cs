using System;
using Aiwins.Rocket.EventBus;

namespace Aiwins.Rocket.Domain.Entities.Events.Distributed
{
    [Serializable]
    [GenericEventName(Postfix = ".Deleted")]
    public class EntityDeletedEto<TEntityEto>
    {
        public TEntityEto Entity { get; set; }

        public EntityDeletedEto(TEntityEto entity)
        {
            Entity = entity;
        }
    }
}