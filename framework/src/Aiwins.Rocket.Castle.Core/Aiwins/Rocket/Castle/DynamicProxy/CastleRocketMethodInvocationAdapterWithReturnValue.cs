using System;
using System.Threading.Tasks;
using Aiwins.Rocket.DynamicProxy;
using Castle.DynamicProxy;

namespace Aiwins.Rocket.Castle.DynamicProxy {
    public class CastleRocketMethodInvocationAdapterWithReturnValue<TResult> : CastleRocketMethodInvocationAdapterBase, IRocketMethodInvocation {
        protected IInvocationProceedInfo ProceedInfo { get; }
        protected Func<IInvocation, IInvocationProceedInfo, Task<TResult>> Proceed { get; }

        public CastleRocketMethodInvocationAdapterWithReturnValue (IInvocation invocation,
            IInvocationProceedInfo proceedInfo,
            Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed) : base (invocation) {
            ProceedInfo = proceedInfo;
            Proceed = proceed;
        }

        public override async Task ProceedAsync () {
            ReturnValue = await Proceed (Invocation, ProceedInfo);
        }
    }
}