using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Aiwins.Rocket.DynamicProxy;
using Castle.DynamicProxy;

namespace Aiwins.Rocket.Castle.DynamicProxy {
    public abstract class CastleRocketMethodInvocationAdapterBase : IRocketMethodInvocation {
        private static readonly ConcurrentDictionary<MethodInfo, bool> isAsyncMethodCache = new ConcurrentDictionary<MethodInfo, bool> ();
        public object[] Arguments => Invocation.Arguments;

        public IReadOnlyDictionary<string, object> ArgumentsDictionary => _lazyArgumentsDictionary.Value;
        private readonly Lazy<IReadOnlyDictionary<string, object>> _lazyArgumentsDictionary;

        public Type[] GenericArguments => Invocation.GenericArguments;

        public object TargetObject => Invocation.InvocationTarget ?? Invocation.MethodInvocationTarget;

        public MethodInfo Method => Invocation.MethodInvocationTarget ?? Invocation.Method;

        public object ReturnValue { get; set; }

        protected IInvocation Invocation { get; }

        protected CastleRocketMethodInvocationAdapterBase (IInvocation invocation) {
            Invocation = invocation;
            _lazyArgumentsDictionary = new Lazy<IReadOnlyDictionary<string, object>> (GetArgumentsDictionary);
        }

        public abstract Task ProceedAsync ();

        public bool IsAsync () {
            if (Invocation == null) {
                throw new ArgumentNullException (nameof (Invocation));
            }

            var isAsyncMethodFromMetaData = isAsyncMethodCache.GetOrAdd (Invocation.Method, IsAsyncMethodFromMetaData);
            if (isAsyncMethodFromMetaData) {
                return true;
            }

            if (Invocation.ReturnValue != null) {
                return IsAsyncType (Invocation.ReturnValue.GetType ().GetTypeInfo ());
            }

            return false;
        }

        private bool IsAsyncMethodFromMetaData (MethodInfo method) {
            if (IsAsyncType (method.ReturnType.GetTypeInfo ())) {
                return true;
            }

            return false;
        }

        private bool IsAsyncType (TypeInfo typeInfo) {
            if (typeInfo.IsTask ()) {
                return true;
            }

            if (typeInfo.IsTaskWithResult ()) {
                return true;
            }

            if (typeInfo.IsValueTask ()) {
                return true;
            }

            if (typeInfo.IsValueTaskWithResult ()) {
                return true;
            }

            return false;
        }

        private IReadOnlyDictionary<string, object> GetArgumentsDictionary () {
            var dict = new Dictionary<string, object> ();

            var methodParameters = Method.GetParameters ();
            for (int i = 0; i < methodParameters.Length; i++) {
                dict[methodParameters[i].Name] = Invocation.Arguments[i];
            }

            return dict;
        }
    }
}