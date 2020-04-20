using Aiwins.Rocket.DynamicProxy;

namespace Aiwins.Rocket.BackgroundJobs {
    /// <summary>
    /// IBackgroundJobManager <see cref="IBackgroundJobManager"/> 相关的扩展方法
    /// </summary>
    public static class BackgroundJobManagerExtensions {
        /// <summary>
        /// 检查后台作业系统是否有真正的实例
        /// 如果实现为 <see cref="NullBackgroundJobManager"/> 则返回false
        /// </summary>
        /// <param name="backgroundJobManager"></param>
        /// <returns></returns>
        public static bool IsAvailable (this IBackgroundJobManager backgroundJobManager) {
            return !(ProxyHelper.UnProxy (backgroundJobManager) is NullBackgroundJobManager);
        }
    }
}