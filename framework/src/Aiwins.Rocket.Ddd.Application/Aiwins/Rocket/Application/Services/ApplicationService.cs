using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aiwins.Rocket.Aspects;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Features;
using Aiwins.Rocket.Guids;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.ObjectMapping;
using Aiwins.Rocket.Settings;
using Aiwins.Rocket.Timing;
using Aiwins.Rocket.Uow;
using Aiwins.Rocket.Users;
using Aiwins.Rocket.Validation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Aiwins.Rocket.Application.Services {
    public abstract class ApplicationService:
        IApplicationService,
        IAvoidDuplicateCrossCuttingConcerns,
        IValidationEnabled,
        IUnitOfWorkEnabled,
        IAuditingEnabled,
        ITransientDependency {
            public IServiceProvider ServiceProvider { get; set; }
            protected readonly object ServiceProviderLock = new object ();

            protected TService LazyGetRequiredService<TService> (ref TService reference) => LazyGetRequiredService (typeof (TService), ref reference);

            protected TRef LazyGetRequiredService<TRef> (Type serviceType, ref TRef reference) {
                if (reference == null) {
                    lock (ServiceProviderLock) {
                        if (reference == null) {
                            reference = (TRef) ServiceProvider.GetRequiredService (serviceType);
                        }
                    }
                }

                return reference;
            }

            public static string[] CommonPostfixes { get; set; } = { "AppService", "ApplicationService", "Service" };

            public List<string> AppliedCrossCuttingConcerns { get; } = new List<string> ();

            public IUnitOfWorkManager UnitOfWorkManager => LazyGetRequiredService (ref _unitOfWorkManager);
            private IUnitOfWorkManager _unitOfWorkManager;

            protected Type ObjectMapperContext { get; set; }
            public IObjectMapper ObjectMapper {
                get {
                    if (_objectMapper != null) {
                        return _objectMapper;
                    }

                    if (ObjectMapperContext == null) {
                        return LazyGetRequiredService (ref _objectMapper);
                    }

                    return LazyGetRequiredService (
                        typeof (IObjectMapper<>).MakeGenericType (ObjectMapperContext),
                        ref _objectMapper
                    );
                }
            }
            private IObjectMapper _objectMapper;

            public IGuidGenerator GuidGenerator { get; set; }

            public ILoggerFactory LoggerFactory => LazyGetRequiredService (ref _loggerFactory);
            private ILoggerFactory _loggerFactory;

            public ICurrentTenant CurrentTenant => LazyGetRequiredService (ref _currentTenant);
            private ICurrentTenant _currentTenant;

            public ICurrentUser CurrentUser => LazyGetRequiredService (ref _currentUser);
            private ICurrentUser _currentUser;

            public ISettingProvider SettingProvider => LazyGetRequiredService (ref _settingProvider);
            private ISettingProvider _settingProvider;

            public IClock Clock => LazyGetRequiredService (ref _clock);
            private IClock _clock;

            public IAuthorizationService AuthorizationService => LazyGetRequiredService (ref _authorizationService);
            private IAuthorizationService _authorizationService;

            public IFeatureChecker FeatureChecker => LazyGetRequiredService (ref _featureChecker);
            private IFeatureChecker _featureChecker;

            public IStringLocalizerFactory StringLocalizerFactory => LazyGetRequiredService (ref _stringLocalizerFactory);
            private IStringLocalizerFactory _stringLocalizerFactory;

            public IStringLocalizer L => _localizer ?? (_localizer = StringLocalizerFactory.Create (LocalizationResource));
            private IStringLocalizer _localizer;

            protected Type LocalizationResource {
                get => _localizationResource;
                set {
                    _localizationResource = value;
                    _localizer = null;
                }
            }
            private Type _localizationResource = typeof (DefaultResource);

            protected IUnitOfWork CurrentUnitOfWork => UnitOfWorkManager?.Current;

            protected ILogger Logger => _lazyLogger.Value;
            private Lazy<ILogger> _lazyLogger => new Lazy<ILogger> (() => LoggerFactory?.CreateLogger (GetType ().FullName) ?? NullLogger.Instance, true);

            protected ApplicationService () {
                GuidGenerator = SimpleGuidGenerator.Instance;
            }

            /// <summary>
            /// 根据策略名称 <paramref name="policyName"/> 验证权限
            /// 当未授权时抛出权限验证异常 <see cref="RocketAuthorizationException"/> 。
            /// </summary>
            /// <param name="policyName">策略名称 <paramref name="policyName"/> 为空不做任何处理</param>
            protected virtual async Task CheckPolicyAsync ([CanBeNull] string policyName) {
                if (string.IsNullOrEmpty (policyName)) {
                    return;
                }

                await AuthorizationService.CheckAsync (policyName);
            }
        }
}