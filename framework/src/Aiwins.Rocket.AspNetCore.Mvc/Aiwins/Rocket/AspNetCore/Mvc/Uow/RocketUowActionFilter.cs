using System;
using System.Net.Http;
using System.Threading.Tasks;
using Aiwins.Rocket.AspNetCore.Uow;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Uow;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AspNetCore.Mvc.Uow {
    public class RocketUowActionFilter : IAsyncActionFilter, ITransientDependency {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RocketUnitOfWorkDefaultOptions _defaultOptions;

        public RocketUowActionFilter (IUnitOfWorkManager unitOfWorkManager, IOptions<RocketUnitOfWorkDefaultOptions> options) {
            _unitOfWorkManager = unitOfWorkManager;
            _defaultOptions = options.Value;
        }

        public async Task OnActionExecutionAsync (ActionExecutingContext context, ActionExecutionDelegate next) {
            if (!context.ActionDescriptor.IsControllerAction ()) {
                await next ();
                return;
            }

            var methodInfo = context.ActionDescriptor.GetMethodInfo ();
            var unitOfWorkAttr = UnitOfWorkHelper.GetUnitOfWorkAttributeOrNull (methodInfo);

            context.HttpContext.Items["_RocketActionInfo"] = new RocketActionInfoInHttpContext {
                IsObjectResult = context.ActionDescriptor.HasObjectResult ()
            };

            if (unitOfWorkAttr?.IsDisabled == true) {
                await next ();
                return;
            }

            var options = CreateOptions (context, unitOfWorkAttr);

            // 开启通过RocketUnitOfWorkMiddleware解析的UnitOfWork
            if (_unitOfWorkManager.TryBeginReserved (RocketUnitOfWorkMiddleware.UnitOfWorkReservationName, options)) {
                var result = await next ();
                if (!Succeed (result)) {
                    await RollbackAsync (context);
                }

                return;
            }

            // 开启一个新的UnitOfWork
            using (var uow = _unitOfWorkManager.Begin (options)) {
                var result = await next ();
                if (Succeed (result)) {
                    await uow.CompleteAsync (context.HttpContext.RequestAborted);
                }
            }
        }

        private RocketUnitOfWorkOptions CreateOptions (ActionExecutingContext context, UnitOfWorkAttribute unitOfWorkAttribute) {
            var options = new RocketUnitOfWorkOptions ();

            unitOfWorkAttribute?.SetOptions (options);

            if (unitOfWorkAttribute?.IsTransactional == null) {
                options.IsTransactional = _defaultOptions.CalculateIsTransactional (
                    autoValue: !string.Equals (context.HttpContext.Request.Method, HttpMethod.Get.Method, StringComparison.OrdinalIgnoreCase)
                );
            }

            return options;
        }

        private async Task RollbackAsync (ActionExecutingContext context) {
            var currentUow = _unitOfWorkManager.Current;
            if (currentUow != null) {
                await currentUow.RollbackAsync (context.HttpContext.RequestAborted);
            }
        }

        private static bool Succeed (ActionExecutedContext result) {
            return result.Exception == null || result.ExceptionHandled;
        }
    }
}