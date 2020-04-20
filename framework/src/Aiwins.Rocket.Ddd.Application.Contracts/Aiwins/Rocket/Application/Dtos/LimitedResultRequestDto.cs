using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aiwins.Rocket.Application.Localization.Resources.RocketDdd;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 实现接口 <see cref="ILimitedResultRequest"/>.
    /// </summary>
    [Serializable]
    public class LimitedResultRequestDto : ILimitedResultRequest, IValidatableObject {
        /// <summary>
        /// 默认值: 10.
        /// </summary>
        public static int DefaultMaxResultCount { get; set; } = 10;

        /// <summary>
        /// 返回数据可能的最大值 <see cref="MaxResultCount"/>.
        /// 默认值: 1,000.
        /// </summary>
        public static int MaxMaxResultCount { get; set; } = 1000;

        /// <summary>
        /// 查询返回数据的最大数量
        /// </summary>
        [Range (1, int.MaxValue)]
        public virtual int MaxResultCount { get; set; } = DefaultMaxResultCount;

        public virtual IEnumerable<ValidationResult> Validate (ValidationContext validationContext) {
            if (MaxResultCount > MaxMaxResultCount) {
                var localizer = validationContext.GetRequiredService<IStringLocalizer<RocketDddApplicationContractsResource>> ();

                yield return new ValidationResult (
                    localizer[
                        "MaxResultCountExceededExceptionMessage",
                        nameof (MaxResultCount),
                        MaxMaxResultCount,
                        typeof (LimitedResultRequestDto).FullName,
                        nameof (MaxMaxResultCount)
                    ],
                    new [] { nameof (MaxResultCount) });
            }
        }
    }
}