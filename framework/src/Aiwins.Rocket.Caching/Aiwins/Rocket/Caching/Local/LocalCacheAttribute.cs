using System;

namespace Aiwins.Rocket.Caching {
    [AttributeUsage (AttributeTargets.Method)]
    public class LocalCacheAttribute : Attribute {
        public string Prefix { get; set; }
        /// <summary>
        /// 缓存有限期，单位：分钟。默认值：60。
        /// </summary>
        public long Expiration { get; set; } = 300;
        /// <summary>
        /// 缓存失效后调用方法时 是否使用线程锁，默认false
        /// </summary>
        public bool ThreadLock { get; set; } = false;
    }
}