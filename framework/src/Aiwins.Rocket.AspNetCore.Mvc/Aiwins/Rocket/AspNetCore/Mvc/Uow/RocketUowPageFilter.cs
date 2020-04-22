using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.AspNetCore.Uow;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Uow;

namespace Aiwins.Rocket.AspNetCore.Mvc.Uow
{
    public class RocketUowPageFilter : IAsyncPageFilter, ITransientDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RocketUnitOfWorkDefaultOptions _defaultOptions;

        public RocketUowPageFilter(IUnitOfWorkManager unitOfWorkManager, IOptions<RocketUnitOfWorkDefaultOptions> options)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _defaultOptions = options.Value;
        }
        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            if (context.HandlerMethod == null || !context.ActionDescriptor.IsPageAction())
            {
                await next();
                return;
            }

            var methodInfo = context.HandlerMethod.MethodInfo;
            var unitOfWorkAttr = UnitOfWorkHelper.GetUnitOfWorkAttributeOrNull(methodInfo);

            context.HttpContext.Items["_RocketActionInfo"] = new RocketActionInfoInHttpContext
            {
                IsObjectResult = ActionResultHelper.IsObjectResult(context.HandlerMethod.MethodInfo.ReturnType)
            };

            if (unitOfWorkAttr?.IsDisabled == true)
            {
                await next();
                return;
            }

            var options = CreateOptions(context, unitOfWorkAttr);

            //Trying to begin a reserved UOW by RocketUnitOfWorkMiddleware
            if (_unitOfWorkManager.TryBeginReserved(RocketUnitOfWorkMiddleware.UnitOfWorkReservationName, options))
            {
                var result = await next();
                if (!Succeed(result))
                {
                    await RollbackAsync(context);
                }

                return;
            }

            //Begin a new, independent unit of work
            using (var uow = _unitOfWorkManager.Begin(options))
            {
                var result = await next();
                if (Succeed(result))
                {
                    await uow.CompleteAsync(context.HttpContext.RequestAborted);
                }
            }
        }
        
        private RocketUnitOfWorkOptions CreateOptions(PageHandlerExecutingContext context, UnitOfWorkAttribute unitOfWorkAttribute)
        {
            var options = new RocketUnitOfWorkOptions();

            unitOfWorkAttribute?.SetOptions(options);

            if (unitOfWorkAttribute?.IsTransactional == null)
            {
                options.IsTransactional = _defaultOptions.CalculateIsTransactional(
                    autoValue: !string.Equals(context.HttpContext.Request.Method, HttpMethod.Get.Method, StringComparison.OrdinalIgnoreCase)
                );
            }

            return options;
        }

        private async Task RollbackAsync(PageHandlerExecutingContext context)
        {
            var currentUow = _unitOfWorkManager.Current;
            if (currentUow != null)
            {
                await currentUow.RollbackAsync(context.HttpContext.RequestAborted);
            }
        }

        private static bool Succeed(PageHandlerExecutedContext result)
        {
            return result.Exception == null || result.ExceptionHandled;
        }
    }
}