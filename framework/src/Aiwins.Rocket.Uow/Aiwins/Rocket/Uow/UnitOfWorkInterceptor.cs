using System;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.DynamicProxy;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Uow {
    public class UnitOfWorkInterceptor : RocketInterceptor, ITransientDependency {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RocketUnitOfWorkDefaultOptions _defaultOptions;

        public UnitOfWorkInterceptor (IUnitOfWorkManager unitOfWorkManager, IOptions<RocketUnitOfWorkDefaultOptions> options) {
            _unitOfWorkManager = unitOfWorkManager;
            _defaultOptions = options.Value;
        }

        public override async Task InterceptAsync (IRocketMethodInvocation invocation) {
            if (!UnitOfWorkHelper.IsUnitOfWorkMethod (invocation.Method, out var unitOfWorkAttribute)) {
                await invocation.ProceedAsync ();
                return;
            }

            using (var uow = _unitOfWorkManager.Begin (CreateOptions (invocation, unitOfWorkAttribute))) {
                await invocation.ProceedAsync ();
                await uow.CompleteAsync ();
            }
        }

        private RocketUnitOfWorkOptions CreateOptions (IRocketMethodInvocation invocation, [CanBeNull] UnitOfWorkAttribute unitOfWorkAttribute) {
            var options = new RocketUnitOfWorkOptions ();

            unitOfWorkAttribute?.SetOptions (options);

            if (unitOfWorkAttribute?.IsTransactional == null) {
                options.IsTransactional = _defaultOptions.CalculateIsTransactional (
                    autoValue: !invocation.Method.Name.StartsWith ("Get", StringComparison.InvariantCultureIgnoreCase)
                );
            }

            return options;
        }
    }
}