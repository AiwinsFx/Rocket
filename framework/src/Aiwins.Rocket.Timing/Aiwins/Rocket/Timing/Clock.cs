using System;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Timing {
    public class Clock : IClock, ITransientDependency {
        protected RocketClockOptions Options { get; }

        public Clock (IOptions<RocketClockOptions> options) {
            Options = options.Value;
        }

        public virtual DateTimeOffset Now => Options.Kind == DateTimeKind.Utc ? DateTimeOffset.UtcNow : DateTimeOffset.Now;

        public virtual DateTimeKind Kind => Options.Kind;

        public virtual bool SupportsMultipleTimezone => Options.Kind == DateTimeKind.Utc;

        public virtual DateTimeOffset Normalize (DateTimeOffset dateTime) {
            if (Kind == DateTimeKind.Unspecified || Kind == dateTime.Kind) {
                return dateTime;
            }

            if (Kind == DateTimeKind.Local && dateTime.Kind == DateTimeKind.Utc) {
                return dateTime.ToLocalTime ();
            }

            if (Kind == DateTimeKind.Utc && dateTime.Kind == DateTimeKind.Local) {
                return dateTime.ToUniversalTime ();
            }

            return DateTime.SpecifyKind (dateTime, Kind);
        }
    }
}