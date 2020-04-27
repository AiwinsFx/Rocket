using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Penguin.Core.Utility;

namespace Penguin.Core.Caching {
    [AttributeUsage (AttributeTargets.Method)]
    public class MemoryCacheAttribute : AbstractInterceptorAttribute {
        private static readonly MethodInfo TaskResultMethod;
        private static readonly IMemoryCache CacheClient;
        private readonly AsyncLock _lock = new AsyncLock ();

        static MemoryCacheAttribute () {
            TaskResultMethod = typeof (Task).GetMethods ().FirstOrDefault (p => p.Name == nameof (Task.FromResult) && p.ContainsGenericParameters);
            CacheClient = new MemoryCache (new MemoryCacheOptions ());
        }

        public string Prefix { get; set; }
        /// <summary>
        /// 缓存有限期，单位：分钟。默认值：60。
        /// </summary>
        public long Expiration { get; set; } = 300;
        /// <summary>
        /// 缓存失效后调用方法时 是否使用线程锁，默认false
        /// </summary>
        public bool ThreadLock { get; set; } = false;

        [FromServiceContext]
        public ILogger<MemoryCacheAttribute> Logger { get; set; }

        public override async Task Invoke (AspectContext context, AspectDelegate next) {

            var parameters = context.ServiceMethod.GetParameters ();
            //判断Method是否包含ref / out参数
            if (parameters.Any (it => it.IsIn || it.IsOut)) {
                await next (context);
            } else {
                var returnType = context.GetReturnType ();
                var key = CacheKeyGenerator.GetCacheKey (context.ServiceMethod, context.Parameters, Prefix);
                try {
                    var resultValue = CacheClient.Get (key);
                    if (resultValue != null) {
                        context.ReturnValue = context.IsAsync () ?
                            TaskResultMethod.MakeGenericMethod (returnType).Invoke (null, new object[] { resultValue }) :
                            resultValue;
                    } else {
                        if (!ThreadLock) {
                            await GetActionResultAndSetCacheValue (context, next, key, returnType);
                        } else {
                            using (await _lock.LockAsync ()) {
                                resultValue = CacheClient.Get (key);
                                if (resultValue != null) {
                                    context.ReturnValue = context.IsAsync () ?
                                        TaskResultMethod.MakeGenericMethod (returnType).Invoke (null, new object[] { resultValue }) :
                                        resultValue;
                                } else {
                                    await GetActionResultAndSetCacheValue (context, next, key, returnType);
                                }
                            }
                        }
                    }
                } catch(Exception ex) {
                    await context.Invoke (next);
                    Logger.LogError(ex.ToString());
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
        public async Task GetActionResultAndSetCacheValue (AspectContext context, AspectDelegate next, string key, Type type) {
            await next (context);

            var result = (await context.GetReturnValue ()) as object;
            if (result != null)
                CacheClient.Set (key, result, TimeSpan.FromMinutes (Expiration));
        }
    }
}