using System;
using Aiwins.Rocket.ObjectExtending;

namespace Aiwins.Rocket.Application.Dtos {
    [Serializable]
    public abstract class ExtensibleEntityDto<TKey> : ExtensibleObject, IEntityDto<TKey> {
        /// <summary>
        /// 实体标识
        /// </summary>
        public TKey Id { get; set; }

        public override string ToString () {
            return $"[DTO: {GetType().Name}] Id = {Id}";
        }
    }

    [Serializable]
    public abstract class ExtensibleEntityDto : ExtensibleObject, IEntityDto {
        public override string ToString () {
            return $"[DTO: {GetType().Name}]";
        }
    }
}