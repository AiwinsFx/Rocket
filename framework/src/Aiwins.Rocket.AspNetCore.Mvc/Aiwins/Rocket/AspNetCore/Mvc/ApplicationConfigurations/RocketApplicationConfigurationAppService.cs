using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Aiwins.Rocket.Application.Services;
using Aiwins.Rocket.AspNetCore.Mvc.MultiTenancy;
using Aiwins.Rocket.Authorization;
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
        private readonly ICurrentUser _currentUser;
        private readonly ISettingProvider _settingProvider;
        private readonly ISettingDefinitionManager _settingDefinitionManager;
        private readonly IFeatureDefinitionManager _featureDefinitionManager;
        private readonly ILanguageProvider _languageProvider;

        public RocketApplicationConfigurationAppService (
            IOptions<RocketLocalizationOptions> localizationOptions,
            IOptions<RocketMultiTenancyOptions> multiTenancyOptions,
            IServiceProvider serviceProvider,
            IRocketAuthorizationPolicyProvider rocketAuthorizationPolicyProvider,
            IAuthorizationService authorizationService,
            ICurrentUser currentUser,
            ISettingProvider settingProvider,
            ISettingDefinitionManager settingDefinitionManager,
            IFeatureDefinitionManager featureDefinitionManager,
            ILanguageProvider languageProvider) {
            _serviceProvider = serviceProvider;
            _rocketAuthorizationPolicyProvider = rocketAuthorizationPolicyProvider;
            _authorizationService = authorizationService;
            _currentUser = currentUser;
            _settingProvider = settingProvider;
            _settingDefinitionManager = settingDefinitionManager;
            _featureDefinitionManager = featureDefinitionManager;
            _languageProvider = languageProvider;
            _localizationOptions = localizationOptions.Value;
            _multiTenancyOptions = multiTenancyOptions.Value;
        }

        public virtual async Task<ApplicationConfigurationDto> GetAsync () {
            //TODO: Optimize & cache..?

            return new ApplicationConfigurationDto {
                Auth = await GetAuthConfigAsync (),
                    Features = await GetFeaturesConfigAsync (),
                    Localization = await GetLocalizationConfigAsync (),
                    CurrentUser = GetCurrentUser (),
                    Setting = await GetSettingConfigAsync (),
                    MultiTenancy = GetMultiTenancy (),
                    CurrentTenant = GetCurrentTenant ()

            };
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
                IsAuthenticated = _currentUser.IsAuthenticated,
                    Id = _currentUser.Id,
                    TenantId = _currentUser.TenantId,
                    UserName = _currentUser.UserName
            };
        }

        protected virtual async Task<ApplicationAuthConfigurationDto> GetAuthConfigAsync () {
            Logger.LogDebug ("Executing RocketApplicationConfigurationAppService.GetAuthConfigAsync()");

            var authConfig = new ApplicationAuthConfigurationDto ();

            var policyNames = await _rocketAuthorizationPolicyProvider.GetPoliciesNamesAsync ();

            Logger.LogDebug ($"GetPoliciesNamesAsync returns {policyNames.Count} items.");

            foreach (var policyName in policyNames) {
                authConfig.Policies[policyName] = true;

                Logger.LogDebug ($"_authorizationService.IsGrantedAsync? {policyName}");

                if (await _authorizationService.IsGrantedAsync (policyName)) {
                    authConfig.GrantedPolicies[policyName] = true;
                }
            }

            Logger.LogDebug ("Executed RocketApplicationConfigurationAppService.GetAuthConfigAsync()");

            return authConfig;
        }

        protected virtual async Task<ApplicationLocalizationConfigurationDto> GetLocalizationConfigAsync () {
            Logger.LogDebug ("Executing RocketApplicationConfigurationAppService.GetLocalizationConfigAsync()");

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

            Logger.LogDebug ("Executed RocketApplicationConfigurationAppService.GetLocalizationConfigAsync()");

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
            Logger.LogDebug ("Executing RocketApplicationConfigurationAppService.GetSettingConfigAsync()");

            var result = new ApplicationSettingConfigurationDto {
                Values = new Dictionary<string, string> ()
            };

            foreach (var settingDefinition in _settingDefinitionManager.GetAll ()) {
                if (!settingDefinition.IsVisibleToClients) {
                    continue;
                }

                result.Values[settingDefinition.Name] = await _settingProvider.GetOrNullAsync (settingDefinition.Name);
            }

            Logger.LogDebug ("Executed RocketApplicationConfigurationAppService.GetSettingConfigAsync()");

            return result;
        }

        protected virtual async Task<ApplicationFeatureConfigurationDto> GetFeaturesConfigAsync () {
            Logger.LogDebug ("Executing RocketApplicationConfigurationAppService.GetFeaturesConfigAsync()");

            var result = new ApplicationFeatureConfigurationDto ();

            foreach (var featureDefinition in _featureDefinitionManager.GetAll ()) {
                if (!featureDefinition.IsVisibleToClients) {
                    continue;
                }

                result.Values[featureDefinition.Name] = await FeatureChecker.GetOrNullAsync (featureDefinition.Name);
            }

            Logger.LogDebug ("Executed RocketApplicationConfigurationAppService.GetFeaturesConfigAsync()");

            return result;
        }
    }
}