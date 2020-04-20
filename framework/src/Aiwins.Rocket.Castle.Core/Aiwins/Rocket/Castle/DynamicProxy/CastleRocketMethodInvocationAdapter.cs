using System;
using System.Threading.Tasks;
using Aiwins.Rocket.DynamicProxy;
using Castle.DynamicProxy;

namespace Aiwins.Rocket.Castle.DynamicProxy {
    public class CastleRocketMethodInvocationAdapter : CastleRocketMethodInvocationAdapterBase, IRocketMethodInvocation {
        protected IInvocationProceedInfo ProceedInfo { get; }
        protected Func<IInvocation, IInvocationProceedInfo, Task> Proceed { get; }

        public CastleRocketMethodInvocationAdapter (IInvocation invocation, IInvocationProceedInfo proceedInfo,
            Func<IInvocation, IInvocationProceedInfo, Task> proceed) : base (invocation) {
            ProceedInfo = proceedInfo;
            Proceed = proceed;
        }

        public override async Task ProceedAsync () {
            await Proceed (Invocation, ProceedInfo);
        }
    }
}