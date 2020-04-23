using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theming
{
    public class ThemeInfo
    {
        public Type ThemeType { get; }

        public string Name { get; }

        public ThemeInfo([NotNull] Type themeType)
        {
            Check.NotNull(themeType, nameof(themeType));

            if (!typeof(ITheme).IsAssignableFrom(themeType))
            {
                throw new RocketException($"Given {nameof(themeType)} ({themeType.AssemblyQualifiedName}) should be type of {typeof(ITheme).AssemblyQualifiedName}");
            }

            ThemeType = themeType;
            Name = ThemeNameAttribute.GetName(themeType);
        }
    }
}