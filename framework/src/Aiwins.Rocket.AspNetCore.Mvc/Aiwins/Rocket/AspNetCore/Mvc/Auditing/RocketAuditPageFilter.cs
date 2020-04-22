﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Aiwins.Rocket.Aspects;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AspNetCore.Mvc.Auditing {
    public class RocketAuditPageFilter : IAsyncPageFilter, ITransientDependency {
        protected RocketAuditingOptions Options { get; }
        private readonly IAuditingHelper _auditingHelper;
        private readonly IAuditingManager _auditingManager;

        public RocketAuditPageFilter (IOptions<RocketAuditingOptions> options, IAuditingHelper auditingHelper, IAuditingManager auditingManager) {
            Options = options.Value;
            _auditingHelper = auditingHelper;
            _auditingManager = auditingManager;
        }

        public Task OnPageHandlerSelectionAsync (PageHandlerSelectedContext context) {
            return Task.CompletedTask;
        }

        public async Task OnPageHandlerExecutionAsync (PageHandlerExecutingContext context, PageHandlerExecutionDelegate next) {
            if (context.HandlerMethod == null || !ShouldSaveAudit (context, out var auditLog, out var auditLogAction)) {
                await next ();
                return;
            }

            using (RocketCrossCuttingConcerns.Applying (context.HandlerInstance, RocketCrossCuttingConcerns.Auditing)) {
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

        private bool ShouldSaveAudit (PageHandlerExecutingContext context, out AuditLogInfo auditLog, out AuditLogActionInfo auditLogAction) {
            auditLog = null;
            auditLogAction = null;

            if (!Options.IsEnabled) {
                return false;
            }

            if (!context.ActionDescriptor.IsPageAction ()) {
                return false;
            }

            var auditLogScope = _auditingManager.Current;
            if (auditLogScope == null) {
                return false;
            }

            if (!_auditingHelper.ShouldSaveAudit (context.HandlerMethod.MethodInfo, true)) {
                return false;
            }

            auditLog = auditLogScope.Log;
            auditLogAction = _auditingHelper.CreateAuditLogAction (
                auditLog,
                context.HandlerMethod.GetType (),
                context.HandlerMethod.MethodInfo,
                context.HandlerArguments
            );

            return true;
        }
    }
}