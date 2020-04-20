using System;

namespace Aiwins.Rocket.Guids {
    /// <summary>
    /// 实现接口IGuidGenerator <see cref="IGuidGenerator"/> ，GUID生成方法 <see cref="Guid.NewGuid"/>。
    /// </summary>
    public class SimpleGuidGenerator : IGuidGenerator {
        public static SimpleGuidGenerator Instance { get; } = new SimpleGuidGenerator ();

        public virtual Guid Create () {
            return Guid.NewGuid ();
        }
    }
}