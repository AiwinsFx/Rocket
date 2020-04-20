using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aiwins.Rocket.Data;
using JetBrains.Annotations;

namespace Aiwins.Rocket.ObjectExtending {
    public class ObjectExtensionPropertyValidationContext {
        /// <summary>
        /// 属性相关扩展信息
        /// </summary>
        [NotNull]
        public ObjectExtensionPropertyInfo ExtensionPropertyInfo { get; }

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
        /// 对象 <see cref="ValidatingObject"/> 验证属性
        /// </summary>
        [CanBeNull]
        public object Value { get; }

        /// <summary>
        /// 通过验证方法 <see cref="IValidatableObject.Validate"/> 验证上下文
        /// </summary>
        [CanBeNull]
        public IServiceProvider ServiceProvider => ValidationContext;

        public ObjectExtensionPropertyValidationContext (
            [NotNull] ObjectExtensionPropertyInfo objectExtensionPropertyInfo, [NotNull] IHasExtraProperties validatingObject, [NotNull] List<ValidationResult> validationErrors, [NotNull] ValidationContext validationContext, [CanBeNull] object value) {
            ExtensionPropertyInfo = Check.NotNull (objectExtensionPropertyInfo, nameof (objectExtensionPropertyInfo));
            ValidatingObject = Check.NotNull (validatingObject, nameof (validatingObject));
            ValidationErrors = Check.NotNull (validationErrors, nameof (validationErrors));
            ValidationContext = Check.NotNull (validationContext, nameof (validationContext));
            Value = value;
        }
    }
}