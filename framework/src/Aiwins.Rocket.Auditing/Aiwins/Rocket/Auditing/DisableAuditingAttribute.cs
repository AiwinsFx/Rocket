using System;

namespace Aiwins.Rocket.Auditing {
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisableAuditingAttribute : Attribute {

    }
}