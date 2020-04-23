using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers
{
    public interface IRocketTagHelperLocalizer : ITransientDependency
    {
        string GetLocalizedText(string text, ModelExplorer explorer);

        IStringLocalizer GetLocalizer(ModelExplorer explorer);

        IStringLocalizer GetLocalizer(Assembly assembly);

        IStringLocalizer GetLocalizer(Type resourceType);
    }
}
