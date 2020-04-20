using System;

namespace Aiwins.Rocket.Timing {
    public class RocketClockOptions {
        /// <summary>
        /// 默认值: <see cref="DateTimeKind.Unspecified"/>
        /// </summary>
        public DateTimeKind Kind { get; set; }

        public RocketClockOptions () {
            Kind = DateTimeKind.Unspecified;
        }
    }
}