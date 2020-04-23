using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.AspNetCore.Mvc.Localization;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers
{
    public class RocketTagHelperLocalizer : IRocketTagHelperLocalizer
    {
        private readonly IStringLocalizerFactory _stringLocalizerFactory;
        private readonly RocketMvcDataAnnotationsLocalizationOptions _options;

        public RocketTagHelperLocalizer(IOptions<RocketMvcDataAnnotationsLocalizationOptions> options, IStringLocalizerFactory stringLocalizerFactory)
        {
            _stringLocalizerFactory = stringLocalizerFactory;
            _options = options.Value;
        }

        public string GetLocalizedText(string text, ModelExplorer explorer)
        {
            var resourceType = GetResourceTypeFromModelExplorer(explorer);
            var localizer = GetStringLocalizer(resourceType);

            return localizer == null ? text : localizer[text].Value;
        }

        public IStringLocalizer GetLocalizer(ModelExplorer explorer)
        {
            var resourceType = GetResourceTypeFromModelExplorer(explorer);
            return GetStringLocalizer(resourceType);
        }

        public IStringLocalizer GetLocalizer(Assembly assembly)
        {
            var resourceType = _options.AssemblyResources.GetOrDefault(assembly);
            return GetStringLocalizer(resourceType);
        }

        public IStringLocalizer GetLocalizer(Type resourceType)
        {
            return GetStringLocalizer(resourceType);
        }

        private IStringLocalizer GetStringLocalizer(Type resourceType)
        {
            return resourceType == null ? null : _stringLocalizerFactory.Create(resourceType);
        }

        private Type GetResourceTypeFromModelExplorer(ModelExplorer explorer)
        {
            var assembly = explorer.Container.ModelType.Assembly;
            return _options.AssemblyResources.GetOrDefault(assembly);
        }
    }
}
