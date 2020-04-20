using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aiwins.Rocket.Data;
using JetBrains.Annotations;

namespace Aiwins.Rocket.ObjectExtending {
    public class ObjectExtensionValidationContext {
        /// <summary>
        /// 对象的相关扩展信息
        /// </summary>
        [NotNull]
        public ObjectExtensionInfo ObjectExtensionInfo { get; }

        /// <summary>
        /// 验证对象
        /// </summary>
        [NotNull]
        public IHasExtraProperties ValidatingObject { get; }

        /// <summary>
        /// 验证错误集合
        /// </summary>
        [NotNull]
        public List<ValidationResult> ValidationErrors { get; }

        /// <summary>
        /// 通过验证方法 <see cref="IValidatableObject.Validate"/> 验证上下文
        /// </summary>
        [NotNull]
        public ValidationContext ValidationContext { get; }

        /// <summary>
        /// 从DI容器中解析应用服务
        /// </summary>
        [CanBeNull]
        public IServiceProvider ServiceProvider => ValidationContext;

        public ObjectExtensionValidationContext (
            [NotNull] ObjectExtensionInfo objectExtensionInfo, [NotNull] IHasExtraProperties validatingObject, [NotNull] List<ValidationResult> validationErrors, [NotNull] ValidationContext validationContext) {
            ObjectExtensionInfo = Check.NotNull (objectExtensionInfo, nameof (objectExtensionInfo));
            ValidatingObject = Check.NotNull (validatingObject, nameof (validatingObject));
            ValidationErrors = Check.NotNull (validationErrors, nameof (validationErrors));
            ValidationContext = Check.NotNull (validationContext, nameof (validationContext));
        }
    }
}