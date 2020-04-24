using System;

namespace Aiwins.Rocket.Timing {
    public interface IClock {
        /// <summary>
        /// 获取现在的时间。
        /// </summary>
        DateTimeOffset Now { get; }

        /// <summary>
        /// 获取时区
        /// </summary>
        DateTimeKind Kind { get; }

        /// <summary>
        /// 是否支持多时区
        /// </summary>
        bool SupportsMultipleTimezone { get; }

        /// <summary>
        /// 时间转换 <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="dateTime">待转换的时间</param>
        /// <returns>转换后的时间</returns>
        DateTimeOffset Normalize (DateTimeOffset dateTime);

        /// <summary>
        /// 时间转换 <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">待转换的时间</param>
        /// <returns>转换后的时间</returns>
        DateTime Normalize (DateTime dateTime);
    }
}