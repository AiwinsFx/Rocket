using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Auditing {
    public interface IMustHaveCreator {
        /// <summary>
        /// 创建者标识
        /// </summary>
        Guid CreatorId { get; set; }
    }

    public interface IMustHaveCreator<TCreator> : IMustHaveCreator {
        /// <summary>
        /// 创建者
        /// </summary>
        [NotNull]
        TCreator Creator { get; set; }
    }
}