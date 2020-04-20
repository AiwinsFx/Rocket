using System;

namespace Aiwins.Rocket.Validation {
    /// <summary>
    /// 禁用自动验证
    /// </summary>
    [AttributeUsage (AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Property)]
    public class DisableValidationAttribute : Attribute {

    }
}