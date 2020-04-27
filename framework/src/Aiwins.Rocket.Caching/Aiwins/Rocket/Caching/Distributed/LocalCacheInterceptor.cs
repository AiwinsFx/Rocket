﻿using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.DynamicProxy;
using Aiwins.Rocket.Reflection;
using Microsoft.Extensions.Caching.Memory;
using Nito.AsyncEx;

namespace Aiwins.Rocket.Caching {
    public class LocalCacheInterceptor : RocketInterceptor, ISingletonDependency {
        private static readonly MethodInfo TaskResultMethod;
        private static readonly IMemoryCache Cache;
        private readonly AsyncLock _lock = new AsyncLock ();

        static LocalCacheInterceptor () {
            TaskResultMethod = typeof (Task).GetMethods ().FirstOrDefault (p => p.Name == nameof (Task.FromResult) && p.ContainsGenericParameters);
            Cache = new MemoryCache (new MemoryCacheOptions ());
        }

        public override async Task InterceptAsync (IRocketMethodInvocation invocation) {
            var parameters = invocation.Method.GetParameters ();
            //判断Method是否包含ref / out参数
            if (parameters.Any (it => it.IsIn || it.IsOut)) {
                await invocation.ProceedAsync ();
            } else {
                // var localCacheAttribute = (LocalCacheAttribute) Attribute.GetCustomAttribute (invocation.Method, typeof (LocalCacheAttribute), false);
                var localCacheAttribute = ReflectionHelper.GetSingleAttributeOrDefault<LocalCacheAttribute> (invocation.Method);
                var cacheKey = CacheKeyGenerator.GetCacheKey (invocation.Method, invocation.Arguments, localCacheAttribute.Prefix);
                var returnType = invocation.Method.ReturnType.GetGenericArguments ().First ();
                try {
                    var resultValue = Cache.Get (cacheKey);
                    if (resultValue != null) {
                        invocation.ReturnValue = invocation.IsAsync () ? TaskResultMethod.MakeGenericMethod (returnType).Invoke (null, new object[] { resultValue }) : resultValue;
                    } else {
                        if (!localCacheAttribute.ThreadLock) {
                            await GetResultAndSetCache (invocation, cacheKey, localCacheAttribute.Expiration);
                        } else {
                            using (await _lock.LockAsync ()) {
                                resultValue = Cache.Get (cacheKey);
                                if (resultValue != null) {
                                    invocation.ReturnValue = invocation.IsAsync () ? TaskResultMethod.MakeGenericMethod (returnType).Invoke (null, new object[] { resultValue }) : resultValue;
                                } else {
                                    await GetResultAndSetCache (invocation, cacheKey, localCacheAttribute.Expiration);
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
                    Cache.Set (cacheKey, invocation.ReturnValue, TimeSpan.FromMinutes (expiration));
            });
        }
    }
}