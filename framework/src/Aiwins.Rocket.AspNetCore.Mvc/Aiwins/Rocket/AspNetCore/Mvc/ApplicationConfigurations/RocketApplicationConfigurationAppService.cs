using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Aiwins.Rocket.Application.Services;
using Aiwins.Rocket.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending;
using Aiwins.Rocket.AspNetCore.Mvc.MultiTenancy;
using Aiwins.Rocket.Authorization;
using Aiwins.Rocket.Caching;
using Aiwins.Rocket.Features;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.Settings;
using Aiwins.Rocket.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AspNetCore.Mvc.ApplicationConfigurations {
    public class RocketApplicationConfigurationAppService : ApplicationService, IRocketApplicationConfigurationAppService {
        private readonly RocketLocalizationOptions _localizationOptions;
        private readonly RocketMultiTenancyOptions _multiTenancyOptions;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRocketAuthorizationPolicyProvider _rocketAuthorizationPolicyProvider;
        private readonly IAuthorizationService _authorizationService;
        private readonly ISettingProvider _settingProvider;
        private readonly ISettingDefinitionManager _settingDefinitionManager;
        private readonly IFeatureDefinitionManager _featureDefinitionManager;
        private readonly ILanguageProvider _languageProvider;
        private readonly ICachedObjectExtensionsDtoService _cachedObjectExtensionsDtoService;

        private readonly IDistributedCache<ApplicationCommonConfigurationCacheItem> _commonConfigurationCache;
        private readonly IDistributedCache<ApplicationPersonalConfigurationCacheItem> _personalConfigurationCache;

        public RocketApplicationConfigurationAppService (
            IOptions<RocketLocalizationOptions> localizationOptions,
            IOptions<RocketMultiTenancyOptions> multiTenancyOptions,
            IServiceProvider serviceProvider,
            IRocketAuthorizationPolicyProvider rocketAuthorizationPolicyProvider,
            IAuthorizationService authorizationService,
            ISettingProvider settingProvider,
            ISettingDefinitionManager settingDefinitionManager,
            IFeatureDefinitionManager featureDefinitionManager,
            ILanguageProvider languageProvider,
            ICachedObjectExtensionsDtoService cachedObjectExtensionsDtoService,
            IDistributedCache<ApplicationCommonConfigurationCacheItem> commonConfigurationCache,
            IDistributedCache<ApplicationPersonalConfigurationCacheItem> personalConfigurationCache) {
            _serviceProvider = serviceProvider;
            _rocketAuthorizationPolicyProvider = rocketAuthorizationPolicyProvider;
            _authorizationService = authorizationService;
            _settingProvider = settingProvider;
            _settingDefinitionManager = settingDefinitionManager;
            _featureDefinitionManager = featureDefinitionManager;
            _languageProvider = languageProvider;
            _cachedObjectExtensionsDtoService = cachedObjectExtensionsDtoService;
            _localizationOptions = localizationOptions.Value;
            _multiTenancyOptions = multiTenancyOptions.Value;
            
            _commonConfigurationCache = commonConfigurationCache;
            _personalConfigurationCache = personalConfigurationCache;
        }

        // public virtual async Task<ApplicationConfigurationDto> GetAsync () {
        //     //TODO: Optimize & cache..?

        //     Logger.LogDebug ("Executing RocketApplicationConfigurationAppService.GetAsync()...");

        //     var result = new ApplicationConfigurationDto {
        //         Auth = await GetAuthConfigAsync (),
        //         Features = await GetFeaturesConfigAsync (),
        //         Localization = await GetLocalizationConfigAsync (),
        //         CurrentUser = GetCurrentUser (),
        //         Setting = await GetSettingConfigAsync (),
        //         MultiTenancy = GetMultiTenancy (),
        //         CurrentTenant = GetCurrentTenant (),
        //         ObjectExtensions = _cachedObjectExtensionsDtoService.Get ()
        //     };

        //     Logger.LogDebug ("Executed RocketApplicationConfigurationAppService.GetAsync().");

        //     return result;
        // }

        public virtual async Task<ApplicationConfigurationDto> GetAsync () {
            var commonConfigurationCacheItem = await GetCommonConfigurationCacheItemAsync ();
            var personalConfigurationCacheItem = await GetPersonalConfigurationCacheItemAsync ();

            var result = new ApplicationConfigurationDto {
                Localization = commonConfigurationCacheItem.Localization,
                Auth = personalConfigurationCacheItem.Auth,
                Features = personalConfigurationCacheItem.Features,
                Setting = personalConfigurationCacheItem.Setting,
                MultiTenancy = GetMultiTenancy (),
                CurrentTenant = GetCurrentTenant (),
                CurrentUser = GetCurrentUser (),
                ObjectExtensions = _cachedObjectExtensionsDtoService.Get ()
            };

            return result;

        }

        protected virtual async Task<ApplicationCommonConfigurationCacheItem> GetCommonConfigurationCacheItemAsync () {
            var cacheKey = CalculateCacheKey ();

            Logger.LogDebug ($"RocketApplicationConfigurationAppService.GetCacheItemAsync: {cacheKey}");

            var cacheItem = await _commonConfigurationCache.GetAsync (cacheKey);

            if (cacheItem != null) {
                Logger.LogDebug ($"Found in the cache: {cacheKey}");
                return cacheItem;
            }

            Logger.LogDebug ($"Not found in the cache, getting from the repository: {cacheKey}");

            cacheItem = new ApplicationCommonConfigurationCacheItem {
                Localization = await GetLocalizationConfigAsync ()
            };

            Logger.LogDebug ($"Setting the cache item: {cacheKey}");

            await _commonConfigurationCache.SetAsync (
                cacheKey,
                cacheItem
            );

            Logger.LogDebug ($"Finished setting the cache item: {cacheKey}");

            return cacheItem;
        }

        protected virtual async Task<ApplicationPersonalConfigurationCacheItem> GetPersonalConfigurationCacheItemAsync () {
            var cacheKey = CalculateCacheKey (CurrentTenant?.Id?.ToString (), CurrentUser?.Id?.ToString ());

            Logger.LogDebug ($"RocketApplicationConfigurationAppService.GetCacheItemAsync: {cacheKey}");

            var cacheItem = await _personalConfigurationCache.GetAsync (cacheKey);

            if (cacheItem != null) {
                Logger.LogDebug ($"Found in the cache: {cacheKey}");
                return cacheItem;
            }

            Logger.LogDebug ($"Not found in the cache, getting from the repository: {cacheKey}");

            cacheItem = new ApplicationPersonalConfigurationCacheItem {
                Auth = await GetAuthConfigAsync (),
                Features = await GetFeaturesConfigAsync (),
                Setting = await GetSettingConfigAsync ()
            };

            Logger.LogDebug ($"Setting the cache item: {cacheKey}");

            await _personalConfigurationCache.SetAsync (
                cacheKey,
                cacheItem
            );

            Logger.LogDebug ($"Finished setting the cache item: {cacheKey}");

            return cacheItem;
        }

        protected virtual CurrentTenantDto GetCurrentTenant () {
            return new CurrentTenantDto () {
                Id = CurrentTenant.Id,
                    Name = CurrentTenant.Name,
                    IsAvailable = CurrentTenant.IsAvailable
            };
        }

        protected virtual MultiTenancyInfoDto GetMultiTenancy () {
            return new MultiTenancyInfoDto {
                IsEnabled = _multiTenancyOptions.IsEnabled
            };
        }

        protected virtual CurrentUserDto GetCurrentUser () {
            return new CurrentUserDto {
                IsAuthenticated = CurrentUser.IsAuthenticated,
                    Id = CurrentUser.Id,
                    TenantId = CurrentUser.TenantId,
                    Name = CurrentUser.Name,
                    UserName = CurrentUser.UserName,
                    Email = CurrentUser.Email,
                    PhoneNumber = CurrentUser.PhoneNumber
            };
        }

        protected virtual async Task<ApplicationAuthConfigurationDto> GetAuthConfigAsync () {
            var authConfig = new ApplicationAuthConfigurationDto ();

            var policyNames = await _rocketAuthorizationPolicyProvider.GetPoliciesNamesAsync ();

            foreach (var policyName in policyNames) {
                authConfig.Policies[policyName] = true;

                if (await _authorizationService.IsGrantedAsync (policyName)) {
                    authConfig.GrantedPolicies[policyName] = true;
                }
            }

            return authConfig;
        }

        protected virtual async Task<ApplicationLocalizationConfigurationDto> GetLocalizationConfigAsync () {
            var localizationConfig = new ApplicationLocalizationConfigurationDto ();

            localizationConfig.Languages.AddRange (await _languageProvider.GetLanguagesAsync ());

            foreach (var resource in _localizationOptions.Resources.Values) {
                var dictionary = new Dictionary<string, string> ();

                var localizer = _serviceProvider.GetRequiredService (
                    typeof (IStringLocalizer<>).MakeGenericType (resource.ResourceType)
                ) as IStringLocalizer;

                foreach (var localizedString in localizer.GetAllStrings ()) {
                    dictionary[localizedString.Name] = localizedString.Value;
                }

                localizationConfig.Values[resource.ResourceName] = dictionary;
            }

            localizationConfig.CurrentCulture = GetCurrentCultureInfo ();

            if (_localizationOptions.DefaultResourceType != null) {
                localizationConfig.DefaultResourceName = LocalizationResourceNameAttribute.GetName (
                    _localizationOptions.DefaultResourceType
                );
            }

            return localizationConfig;
        }

        private static CurrentCultureDto GetCurrentCultureInfo () {
            return new CurrentCultureDto {
                Name = CultureInfo.CurrentUICulture.Name,
                    DisplayName = CultureInfo.CurrentUICulture.DisplayName,
                    EnglishName = CultureInfo.CurrentUICulture.EnglishName,
                    NativeName = CultureInfo.CurrentUICulture.NativeName,
                    IsRightToLeft = CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft,
                    CultureName = CultureInfo.CurrentUICulture.TextInfo.CultureName,
                    TwoLetterIsoLanguageName = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName,
                    ThreeLetterIsoLanguageName = CultureInfo.CurrentUICulture.ThreeLetterISOLanguageName,
                    DateTimeFormat = new DateTimeFormatDto {
                        CalendarAlgorithmType = CultureInfo.CurrentUICulture.DateTimeFormat.Calendar.AlgorithmType.ToString (),
                        DateTimeFormatLong = CultureInfo.CurrentUICulture.DateTimeFormat.LongDatePattern,
                        ShortDatePattern = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern,
                        FullDateTimePattern = CultureInfo.CurrentUICulture.DateTimeFormat.FullDateTimePattern,
                        DateSeparator = CultureInfo.CurrentUICulture.DateTimeFormat.DateSeparator,
                        ShortTimePattern = CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern,
                        LongTimePattern = CultureInfo.CurrentUICulture.DateTimeFormat.LongTimePattern,
                        }
            };
        }

        private async Task<ApplicationSettingConfigurationDto> GetSettingConfigAsync () {
            var result = new ApplicationSettingConfigurationDto {
                Values = new Dictionary<string, string> ()
            };

            foreach (var settingDefinition in _settingDefinitionManager.GetAll ()) {
                if (!settingDefinition.IsVisibleToClients) {
                    continue;
                }

                result.Values[settingDefinition.Name] = await _settingProvider.GetOrNullAsync (settingDefinition.Name);
            }

            return result;
        }

        protected virtual async Task<ApplicationFeatureConfigurationDto> GetFeaturesConfigAsync () {
            var result = new ApplicationFeatureConfigurationDto ();

            foreach (var featureDefinition in _featureDefinitionManager.GetAll ()) {
                if (!featureDefinition.IsVisibleToClients) {
                    continue;
                }

                result.Values[featureDefinition.Name] = await FeatureChecker.GetOrNullAsync (featureDefinition.Name);
            }

            return result;
        }

        protected virtual string CalculateCacheKey () {
            return ApplicationCommonConfigurationCacheItem.CalculateCacheKey ();
        }

        protected virtual string CalculateCacheKey (string tenantId, string userId) {
            return ApplicationPersonalConfigurationCacheItem.CalculateCacheKey (tenantId, userId);
        }
    }
}