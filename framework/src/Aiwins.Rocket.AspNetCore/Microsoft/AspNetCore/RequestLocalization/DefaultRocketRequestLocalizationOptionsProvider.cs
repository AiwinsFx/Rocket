using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Settings;
using Aiwins.Rocket.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Nito.AsyncEx;

namespace Microsoft.AspNetCore.RequestLocalization {
    public class DefaultRocketRequestLocalizationOptionsProvider : IRocketRequestLocalizationOptionsProvider, ISingletonDependency {
        private readonly IServiceScopeFactory _serviceProviderFactory;
        private readonly SemaphoreSlim _syncSemaphore;
        private Action<RequestLocalizationOptions> _optionsAction;
        private RequestLocalizationOptions _requestLocalizationOptions;

        public DefaultRocketRequestLocalizationOptionsProvider (IServiceScopeFactory serviceProviderFactory) {
            _serviceProviderFactory = serviceProviderFactory;
            _syncSemaphore = new SemaphoreSlim (1, 1);
        }

        public void InitLocalizationOptions (Action<RequestLocalizationOptions> optionsAction = null) {
            _optionsAction = optionsAction;
        }

        public RequestLocalizationOptions GetLocalizationOptions () {
            if (_requestLocalizationOptions != null) {
                return _requestLocalizationOptions;
            }

            return AsyncHelper.RunSync (GetLocalizationOptionsAsync);
        }

        public async Task<RequestLocalizationOptions> GetLocalizationOptionsAsync () {
            if (_requestLocalizationOptions == null) {
                using (await _syncSemaphore.LockAsync ()) {
                    using (var serviceScope = _serviceProviderFactory.CreateScope ()) {
                        var languageProvider = serviceScope.ServiceProvider.GetRequiredService<ILanguageProvider> ();
                        var settingProvider = serviceScope.ServiceProvider.GetRequiredService<ISettingProvider> ();

                        var languages = await languageProvider.GetLanguagesAsync ();
                        var defaultLanguage = await settingProvider.GetOrNullAsync (LocalizationSettingNames.DefaultLanguage);

                        var options = !languages.Any () ?
                            new RequestLocalizationOptions () :
                            new RequestLocalizationOptions {
                                DefaultRequestCulture = DefaultGetRequestCulture (defaultLanguage, languages),

                                SupportedCultures = languages
                                .Select (l => l.CultureName)
                                .Distinct ()
                                .Select (c => new CultureInfo (c))
                                .ToArray (),

                                SupportedUICultures = languages
                                .Select (l => l.UiCultureName)
                                .Distinct ()
                                .Select (c => new CultureInfo (c))
                                .ToArray ()
                            };

                        _optionsAction?.Invoke (options);
                        _requestLocalizationOptions = options;
                    }
                }
            }

            return _requestLocalizationOptions;
        }

        private static RequestCulture DefaultGetRequestCulture (string defaultLanguage, IReadOnlyList<LanguageInfo> languages) {
            if (defaultLanguage == null) {
                var firstLanguage = languages.First ();
                return new RequestCulture (firstLanguage.CultureName, firstLanguage.UiCultureName);
            }

            var (cultureName, uiCultureName) = LocalizationSettingHelper.ParseLanguageSetting (defaultLanguage);
            return new RequestCulture (cultureName, uiCultureName);
        }
    }
}