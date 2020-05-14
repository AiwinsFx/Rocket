using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.DynamicProxy;
using Aiwins.Rocket.Reflection;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Nito.AsyncEx;

namespace Aiwins.Rocket.Caching {
    public class DistributedCacheInterceptor : RocketInterceptor, ISingletonDependency {
        private readonly IDistributedCache _cache;
        private readonly IDistributedCacheSerializer _serializer;
        private static readonly MethodInfo _taskResultMethod = typeof (Task).GetMethods ().FirstOrDefault (p => p.Name == nameof (Task.FromResult) && p.ContainsGenericParameters);
        private readonly AsyncLock _lock = new AsyncLock ();

        public DistributedCacheInterceptor (
            IDistributedCache cache,
            IDistributedCacheSerializer serializer
        ) {
            _cache = cache;
            _serializer = serializer;
        }

        public override async Task InterceptAsync (IRocketMethodInvocation invocation) {
            var parameters = invocation.Method.GetParameters ();
            //判断Method是否包含ref / out参数
            if (parameters.Any (it => it.IsIn || it.IsOut)) {
                await invocation.ProceedAsync ();
            } else {
                var distributedCacheAttribute = ReflectionHelper.GetSingleAttributeOrDefault<LocalCacheAttribute> (invocation.Method);
                var cacheKey = CacheKeyHelper.GetCacheKey (invocation.Method, invocation.Arguments, distributedCacheAttribute.Prefix);
                var returnType = invocation.Method.ReturnType.GetGenericArguments ().First ();
                try {
                    var cacheValue = await _cache.GetAsync (cacheKey);
                    if (cacheValue != null) {
                        var resultValue = _serializer.Deserialize (cacheValue, returnType);
                        invocation.ReturnValue = invocation.IsAsync () ? _taskResultMethod.MakeGenericMethod (returnType).Invoke (null, new object[] { resultValue }) : resultValue;
                    } else {
                        if (!distributedCacheAttribute.ThreadLock) {
                            await GetResultAndSetCache (invocation, cacheKey, distributedCacheAttribute.Expiration);
                        } else {
                            using (await _lock.LockAsync ()) {
                                cacheValue = await _cache.GetAsync (cacheKey);
                                if (cacheValue != null) {
                                    var resultValue = _serializer.Deserialize (cacheValue, returnType);
                                    invocation.ReturnValue = invocation.IsAsync () ? _taskResultMethod.MakeGenericMethod (returnType).Invoke (null, new object[] { resultValue }) : resultValue;
                                } else {
                                    await GetResultAndSetCache (invocation, cacheKey, distributedCacheAttribute.Expiration);
                                }
                            }
                        }
                    }
                } catch (Exception) {
                    await invocation.ProceedAsync ();
                }
            }
        }

        /// <summary>
        /// 直接调用方法，并把结果加入缓存
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <param name="key">缓存key</param>
        /// <param name="type">缓存值类型</param>
        /// <returns></returns>
        public async Task GetResultAndSetCache (IRocketMethodInvocation invocation, string cacheKey, long expiration) {
            await invocation.ProceedAsync ().ContinueWith (task => {
                if (invocation.ReturnValue != null)
                    _cache.SetAsync (cacheKey, _serializer.Serialize (invocation.ReturnValue), new DistributedCacheEntryOptions ().SetSlidingExpiration (TimeSpan.FromMinutes (expiration)));
            });
        }
    }
}