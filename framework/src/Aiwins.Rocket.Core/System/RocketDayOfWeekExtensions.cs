namespace System {
    /// <summary>
    /// DayOfWeek <see cref="DayOfWeek"/> 相关的扩展方法。
    /// </summary>
    public static class RocketDayOfWeekExtensions {
        /// <summary>
        /// 判断值 <see cref="DayOfWeek"/> 是否是周末 
        /// </summary>
        public static bool IsWeekend (this DayOfWeek dayOfWeek) {
            return dayOfWeek.IsIn (DayOfWeek.Saturday, DayOfWeek.Sunday);
        }

        /// <summary>
        /// 判断值 <see cref="DayOfWeek"/> 是否是工作日
        /// </summary>
        public static bool IsWeekday (this DayOfWeek dayOfWeek) {
            return dayOfWeek.IsIn (DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday);
        }
    }
}