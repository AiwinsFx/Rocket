using System;

namespace Aiwins.Rocket.Application.Dtos {
    [Serializable]
    public abstract class EntityDto : IEntityDto //TODO: 考虑删除这个类
    {
        public override string ToString () {
            return $"[DTO: {GetType().Name}]";
        }
    }

    [Serializable]
    public abstract class EntityDto<TKey> : EntityDto, IEntityDto<TKey> {
        /// <summary>
        /// 实体标识
        /// </summary>
        public TKey Id { get; set; }

        public override string ToString () {
            return $"[DTO: {GetType().Name}] Id = {Id}";
        }
    }
}