using System;

namespace Aiwins.Rocket.Timing {
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class DisableDateTimeNormalizationAttribute : Attribute {

    }
}