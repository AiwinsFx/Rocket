using System;

namespace Aiwins.Rocket.Guids {
    /// <summary>
    /// 用于生成Guid
    /// </summary>
    public interface IGuidGenerator {
        /// <summary>
        /// 创建Guid <see cref="Guid"/>.
        /// </summary>
        Guid Create ();
    }
}