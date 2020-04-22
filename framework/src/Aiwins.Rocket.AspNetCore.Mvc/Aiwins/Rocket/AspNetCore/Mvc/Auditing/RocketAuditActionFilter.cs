using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Aiwins.Rocket.Aspects;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AspNetCore.Mvc.Auditing {
    public class RocketAuditActionFilter : IAsyncActionFilter, ITransientDependency {
        protected RocketAuditingOptions Options { get; }
        private readonly IAuditingHelper _auditingHelper;
        private readonly IAuditingManager _auditingManager;

        public RocketAuditActionFilter (IOptions<RocketAuditingOptions> options, IAuditingHelper auditingHelper, IAuditingManager auditingManager) {
            Options = options.Value;
            _auditingHelper = auditingHelper;
            _auditingManager = auditingManager;
        }

        public async Task OnActionExecutionAsync (ActionExecutingContext context, ActionExecutionDelegate next) {
            if (!ShouldSaveAudit (context, out var auditLog, out var auditLogAction)) {
                await next ();
                return;
            }

            using (RocketCrossCuttingConcerns.Applying (context.Controller, RocketCrossCuttingConcerns.Auditing)) {
                var stopwatch = Stopwatch.StartNew ();

                try {
                    var result = await next ();

                    if (result.Exception != null && !result.ExceptionHandled) {
                        auditLog.Exceptions.Add (result.Exception);
                    }
                } catch (Exception ex) {
                    auditLog.Exceptions.Add (ex);
                    throw;
                } finally {
                    stopwatch.Stop ();
                    auditLogAction.ExecutionDuration = Convert.ToInt32 (stopwatch.Elapsed.TotalMilliseconds);
                    auditLog.Actions.Add (auditLogAction);
                }
            }
        }

        private bool ShouldSaveAudit (ActionExecutingContext context, out AuditLogInfo auditLog, out AuditLogActionInfo auditLogAction) {
            auditLog = null;
            auditLogAction = null;

            if (!Options.IsEnabled) {
                return false;
            }

            if (!context.ActionDescriptor.IsControllerAction ()) {
                return false;
            }

            var auditLogScope = _auditingManager.Current;
            if (auditLogScope == null) {
                return false;
            }

            if (!_auditingHelper.ShouldSaveAudit (context.ActionDescriptor.GetMethodInfo (), true)) {
                return false;
            }

            auditLog = auditLogScope.Log;
            auditLogAction = _auditingHelper.CreateAuditLogAction (
                auditLog,
                context.ActionDescriptor.AsControllerActionDescriptor ().ControllerTypeInfo.AsType (),
                context.ActionDescriptor.AsControllerActionDescriptor ().MethodInfo,
                context.ActionArguments
            );

            return true;
        }
    }
}