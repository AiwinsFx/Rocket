using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Auditing {
    public interface IMayHaveCreator {
        /// <summary>
        /// 创建者标识
        /// </summary>
        Guid? CreatorId { get; set; }
    }

    public interface IMayHaveCreator<TCreator> {
        [CanBeNull]
        TCreator Creator { get; set; }
    }
}