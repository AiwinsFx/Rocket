using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theming
{
    public class ThemeDictionary : Dictionary<Type, ThemeInfo>
    {
        public ThemeInfo Add<TTheme>()
            where TTheme : ITheme
        {
            return Add(typeof(TTheme));
        }

        public ThemeInfo Add(Type themeType)
        {
            if (ContainsKey(themeType))
            {
                throw new RocketException("This theme is already added before: " + themeType.AssemblyQualifiedName);
            }

            return this[themeType] = new ThemeInfo(themeType);
        }
    }
}