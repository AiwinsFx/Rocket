using System;
using System.Collections.Generic;
using Aiwins.Rocket.Aspects;
using Aiwins.Rocket.AspNetCore.Mvc.Validation;
using Aiwins.Rocket.Features;
using Aiwins.Rocket.Guids;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.ObjectMapping;
using Aiwins.Rocket.Timing;
using Aiwins.Rocket.Uow;
using Aiwins.Rocket.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Aiwins.Rocket.AspNetCore.Mvc {
    public abstract class RocketController : Controller, IAvoidDuplicateCrossCuttingConcerns {
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

        public IGuidGenerator GuidGenerator => LazyGetRequiredService (ref _guidGenerator);
        private IGuidGenerator _guidGenerator;

        public ILoggerFactory LoggerFactory => LazyGetRequiredService (ref _loggerFactory);
        private ILoggerFactory _loggerFactory;

        public ICurrentUser CurrentUser => LazyGetRequiredService (ref _currentUser);
        private ICurrentUser _currentUser;

        public ICurrentTenant CurrentTenant => LazyGetRequiredService (ref _currentTenant);
        private ICurrentTenant _currentTenant;

        public IAuthorizationService AuthorizationService => LazyGetRequiredService (ref _authorizationService);
        private IAuthorizationService _authorizationService;

        protected IUnitOfWork CurrentUnitOfWork => UnitOfWorkManager?.Current;

        public IClock Clock => LazyGetRequiredService (ref _clock);
        private IClock _clock;

        public IModelStateValidator ModelValidator => LazyGetRequiredService (ref _modelValidator);
        private IModelStateValidator _modelValidator;

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

        public List<string> AppliedCrossCuttingConcerns { get; } = new List<string> ();

        protected virtual void ValidateModel () {
            ModelValidator?.Validate (ModelState);
        }

        protected ILogger Logger => _lazyLogger.Value;
        private Lazy<ILogger> _lazyLogger => new Lazy<ILogger> (() => LoggerFactory?.CreateLogger (GetType ().FullName) ?? NullLogger.Instance, true);
    }
}